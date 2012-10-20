
ECHO off

c:\windows\Microsoft.Net\Framework\v3.5\MSBuild SmartSpider10.sln /t:Rebuild

del SmartSpider10.sln.cache
del /s SmartSpider\bin\Smart*.pdb
del /s SmartSpider\bin\Smart*.xml
del /s SmartDBUtility\bin\Smart*.pdb
del /s SmartDBUtility\bin\Smart*.xml
del /s SmartDBUtility\bin\Smart*.dll
rd /s /q SmartDBUtility\obj
rd /s /q SmartSecurity\bin
rd /s /q SmartSecurity\obj
rd /s /q SmartSpiderConfig\bin
rd /s /q SmartSpiderConfig\obj
rd /s /q SmartSpiderIUtility\bin
rd /s /q SmartSpiderIUtility\obj
rd /s /q SmartUtility\bin
rd /s /q SmartUtility\obj
rd /s /q SmartSpider\obj