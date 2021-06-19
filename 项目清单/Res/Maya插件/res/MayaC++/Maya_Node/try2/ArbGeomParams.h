#ifndef _Alembic_Arnold_ArbGeomParams_h_
#define _Alembic_Arnold_ArbGeomParams_h_
#include <ai.h>
#include <Alembic/AbcGeom/All.h>
using namespace Alembic::AbcGeom;
void AddArbitraryGeomParams(ICompoundProperty& parent,
    ISampleSelector& sampleSelector,
    AtNode* primNode,
    const std::set<std::string>* excludeNames = NULL
);
inline AtArray* ArrayConvert(AtUInt32 nelements, AtByte nkeys, AtByte type, void* data)
{
#if AI_VERSION_ARCH_NUM < 4
    return AiArrayConvert(nelements, nkeys, type, data, TRUE);
#else
    // Arnold 4.x essentially hardcodes the last param to true
    return AiArrayConvert(nelements, nkeys, type, data);
#endif
}
#endif
