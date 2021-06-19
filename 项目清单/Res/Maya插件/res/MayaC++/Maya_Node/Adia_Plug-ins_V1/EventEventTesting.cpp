#include <stdio.h>

#define OPENMAYA_EXPORT
#pragma warning(disable:4996)
#include <maya/MIOStream.h>
#include <maya/MGlobal.h>
#include <maya/MString.h>
#include <maya/MStringArray.h>
#include <maya/MEventMessage.h>
#include <maya/MFnPlugin.h>

#include <maya/MPxCommand.h>
#include <maya/MSyntax.h>
#include <maya/MArgDatabase.h>

//
// Definitions
//

// Message flag
//
#define kMessageFlag            "m"
#define kMessageFlagLong        "message"

//
// Declarations
//
namespace MyEventTesting 
{
	// Callback function for messages
//
	static void eventCBB(void* data);

	// Array of callback ids.
	//
	typedef MCallbackId* MCallbackIdPtr_t;
	static MCallbackIdPtr_t callbackId = NULL;

	// Array of event names.
	//
	static MStringArray eventNames;

	//
	// Command class declaration
	//
	class eventTestTT : public MPxCommand
	{
	public:
		eventTestTT();
		virtual                 ~eventTestTT();

		MStatus                 doIt(const MArgList& args);

		static MSyntax                  newSyntax();
		static void* creator();

	private:
		MStatus                 parseArgs(const MArgList& args);

		bool                                    addMessage;
		bool                                    delMessage;

		MStringArray                    events;
	};


	//
	// Command class implementation
	//

	// Constructor
	//
	eventTestTT::eventTestTT()
		: addMessage(false)
		, delMessage(false)
	{
		events.clear();
	}

	// Destructor
	//
	eventTestTT::~eventTestTT()
	{
		// Do nothing
	}

	// creator
	//
	void* eventTestTT::creator()
	{
		return (void*)(new eventTestTT);
	}

	// newSyntax
	//
	MSyntax eventTestTT::newSyntax()
	{
		MSyntax syntax;

		syntax.addFlag(kMessageFlag, kMessageFlagLong, MSyntax::kBoolean);
		syntax.setObjectType(MSyntax::kStringObjects);

		return syntax;
	}

	// parseArgs
	//
	MStatus eventTestTT::parseArgs(const MArgList& args)
	{
		MStatus                 status;
		MArgDatabase    argData(syntax(), args);

		if (argData.isFlagSet(kMessageFlag))
		{
			bool flag;

			status = argData.getFlagArgument(kMessageFlag, 0, flag);
			if (!status)
			{
				status.perror("could not parse message flag");
				return status;
			}

			if (flag)
			{
				addMessage = true;
			}
			else
			{
				delMessage = true;
			}
		}

		status = argData.getObjects(events);
		if (!status)
		{
			status.perror("could not parse event names");
		}

		// If there are no events specified, operate on all of them
		//
		if (events.length() == 0)
		{
			// eventNames is set in initializePlugin to all the
			// currently available event names.
			//
			events = eventNames;
		}

		return status;
	}

	// doIt
	//
	MStatus eventTestTT::doIt(const MArgList& args)
	{
		MStatus status;

		status = parseArgs(args);
		if (!status)
		{
			return status;
		}

		// Allocate an array of indices.  events[n] is a user provided
		// event name.  Look it up in the static eventNames array
		// and set indices[n] to the index of the entry in eventNames.
		//
		// This maps the user specified events to the global events
		// so we can track callback adds and removes globally.
		//
		int* indices = new int[events.length()];

		int i, j;

		for (i = 0; i < (int)events.length(); ++i)
		{
			// Initialize the entry to "not found".
			//
			indices[i] = -1;

			// Search event names for a match.
			//
			for (j = 0; j < (int)eventNames.length(); ++j)
			{
				if (events[i] == eventNames[j])
				{
					// Found a match.  Store the index and stop looking for
					//
					indices[i] = j;
					break;
				}
			}
		}

		for (i = 0; i < (int)events.length(); ++i)
		{
			j = indices[i];
			if (j == -1)
			{
				MGlobal::displayWarning(events[i] +
					MString("is not a valid event name\n"));
				break;
			}

			if (addMessage && callbackId[j] == 0)
			{
				callbackId[j] = MEventMessage::addEventCallback(
					events[i],
					eventCBB,
					(void*)(size_t)j,
					&status);

				if (!status)
				{
					status.perror("failed to add callback for " + events[i]);
					callbackId[j] = 0;
				}
			}
			else if (delMessage && callbackId[j] != 0)
			{
				status = MMessage::removeCallback(callbackId[j]);

				if (!status)
				{
					status.perror("failed to remove callback for " + events[i]);
				}

				callbackId[j] = 0;
			}
		}

		// Ok, we've made all the necessary changes.  Now show the status.
		//

		MGlobal::displayInfo("Event Name            Msgs On\n");
		MGlobal::displayInfo("--------------------  -------\n");

		char tmpStr[128];
		bool msgs;

		for (i = 0; i < (int)events.length(); ++i)
		{
			j = indices[i];
			if (j == -1)
			{
				continue;
			}

			msgs = (callbackId[j] != 0);

			sprintf(tmpStr, "%-20s  %s\n",
				events[i].asChar(),
				msgs ? "yes" : "no");

			MGlobal::displayInfo(tmpStr);
		}

		// Free up the indices we allocated.
		//
		delete[] indices;

		return status;
	}

	//
	// Plug-in functions
	//

	


	//
	// Callback function
	//

	static void eventCBB(void* data)
	{
		int i = (int)(size_t)data;

		if (i >= 0 && i < (int)eventNames.length())
		{
			MGlobal::displayInfo("event " +
				eventNames[i] +
				" occurred\n");
		}
		else
		{
			MGlobal::displayWarning("BOGUS client data in eventCB!\n");
		}
	}
}
