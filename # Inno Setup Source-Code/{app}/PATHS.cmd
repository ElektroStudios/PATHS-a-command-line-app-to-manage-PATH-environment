@Echo Off

:: Title & Size
Title %~nx0
Mode CON Cols=150 Lines=60

:: NirCMD 
If Exist "%SystemRoot%\System32\Nircmd.exe" (
	"%SystemRoot%\System32\Nircmd.exe" win center ititle "%~nx0"
)

:: CLI Help
CMD /K "%~n0.exe" ^| MORE