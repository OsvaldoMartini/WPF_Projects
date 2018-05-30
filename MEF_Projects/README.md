MEF - Projects
Managed Extensibility Framework (MEF)


Very Important

  Set All Projects ("Exporting Libraries")
 
   Project Properties->Build option and set the building path of 
   
   each application to the bin\Debug folder of the MEF_Application project
   
   or Host Project (Final Project or Project that will use the plugin)
   
   You can when you execute the MEF_Application project it will show a two component(s) imported successfully message and rest messages. 
   
   If you remove any of the libraries from ExportingLib1.dll or ExportingLib2.dll, 
   
   the message will change to 1 component(s) are imported successfully.

   
   Project-> Properties -> Build -> Output -> Output Path : ..\MEF_Application\bin\Debug\