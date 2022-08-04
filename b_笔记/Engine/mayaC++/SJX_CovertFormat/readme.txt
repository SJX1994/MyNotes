1) Building the ViewScene sample on Windows using Visual Studio 2015 (and the default settings):

    1. cd samples\ViewScene
    2. mkdir build
    3. cd build
    4. cmake -G "Visual Studio 14 Win64" ..

    5. Now that the ViewScene.sln and all the other related files have been generated in the samples\ViewScene\build,
       load the solution in Visual Studio and build it. The build result will be written in the bin directory at the root
       level of the FBX SDK installation.

If you want to re-generate the solution with different settings, it is preferable that you first delete the content of
the build folder to avoid cmake cache incompatibilities.

2) Building the ViewScene sample on Windows using Visual Studio 2015 32bits /MT

    cmake -G "Visual Studio 14" -DFBX_STATIC_RTL=1 ..
    
3) Building the ViewScene sample on Windows using Visual Studio 2015 64 bits DLL

    cmake -G "Visual Studio 14 Win64" -DFBX_SHARED=1 ..
    
4) Wrong configuration, will display a warning and generates a DLL version

Generators
  Visual Studio 17 2022        = Generates Visual Studio 2022 project files.
                                 Use -A option to specify architecture.
* Visual Studio 16 2019        = Generates Visual Studio 2019 project files.
                                 Use -A option to specify architecture.
  Visual Studio 15 2017 [arch] = Generates Visual Studio 2017 project files.
                                 Optional [arch] can be "Win64" or "ARM".
  Visual Studio 14 2015 [arch] = Generates Visual Studio 2015 project files.
                                 Optional [arch] can be "Win64" or "ARM".
  Visual Studio 12 2013 [arch] = Generates Visual Studio 2013 project files.
                                 Optional [arch] can be "Win64" or "ARM".
  Visual Studio 11 2012 [arch] = Generates Visual Studio 2012 project files.
                                 Optional [arch] can be "Win64" or "ARM".
  Visual Studio 10 2010 [arch] = Deprecated.  Generates Visual Studio 2010
                                 project files.  Optional [arch] can be
                                 "Win64" or "IA64".
  Visual Studio 9 2008 [arch]  = Generates Visual Studio 2008 project files.