SET folder=BookLibrary.Course
SET zipName=BookLibrary.zip

SET zip="tools\7za.exe" a -mcu -xr!obj -xr!bin -xr!.vs -xr!*.exercise.zip -x!%folder%/submitions -x!%folder%/submissions -x!%folder%/html

del %zipName%

%zip% %zipName% %folder%
