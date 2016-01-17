write-host "*** Exchange DkimSigner Install Script ***" -f "blue"

# Exchange 2013     (15.0.516.32)
# Exchange 2013 CU1 (15.0.620.29)
# Exchange 2013 CU2 (15.0.712.24)
# Exchange 2013 CU3 (15.0.775.38)
# Exchange 2013 SP1 CU4 (15.0.847.32)
# Exchange 2013 SP1 CU5 (15.0.913.22)
# Exchange 2013 SP1 CU6 (15.0.995.29)
# Exchange 2013 SP1 CU7 (15.0.1044.25)
# Exchange 2013 SP1 CU8 (15.0.1076.9)
# Exchange 2013 SP1 CU9 (15.0.1104.5)
# Exchange 2013 SP1 CU10 (15.0.1130.7)
# Exchange 2016 Preview	 (15.01.225.017)
# Exchange 2016 RTM	     (15.01.225.042)
write-host "Detecting Exchange version ... " -f "cyan"
$hostname = hostname
$exchserver = Get-ExchangeServer -Identity $hostname
$EXDIR="C:\Program Files\Exchange Greylist Agent" 
$EXVER="Unknown"
if (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 516) {
	$EXVER="2013"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 620) {
	$EXVER="2013CU1"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 712) {
	$EXVER="2013CU2"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 775) {
	$EXVER="2013CU3"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 847) {
	$EXVER="2013SP1CU4"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 913) {
	$EXVER="2013SP13CU5"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 995) {
	$EXVER="2013SP1CU6"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 1044) {
	$EXVER="2013SP1CU7"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 1076) {
	$EXVER="2013SP1CU8"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 1104) {
	$EXVER="2013SP1CU9"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 1130) {
	$EXVER="2013SP1CU10"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 0 -and ($exchserver.admindisplayversion).build -eq 1156) {
	$EXVER="2013SP1CU11"
} elseif (($exchserver.admindisplayversion).major -eq 15 -and ($exchserver.admindisplayversion).minor -eq 1 -and ($exchserver.admindisplayversion).build -eq 225 -and ($exchserver.admindisplayversion).revision -eq 42) {
	$EXVER="2016RTM"
}
else {
	throw "The exchange version is not yet supported: " + $exchserver.admindisplayversion
}

write-host "Found $EXVER" -f "green"

$SRCDIR="src\GreylistAgent\bin\$EXVER"

net stop MSExchangeTransport 
 
write-host "Creating install directory: '$EXDIR' and copying data from '$SRCDIR'"  -f "green"
new-item -Type Directory -path $EXDIR -ErrorAction SilentlyContinue 

copy-item "$SRCDIR\*" $EXDIR -force
copy-item "Src\GreylistAgent.Configurator\bin\Release\*" $EXDIR -force

# Unblocks files that were downloaded from the Internet.
#unblock-file "$EXDIR\ExchangeDkimSigner.dll"
#unblock-file "$EXDIR\ExchangeDkimSigner.pdb"
#unblock-file "$EXDIR\settings.xml"

$EXDIR\GreylistAgent.Configurator.exe -register
read-host "Now open 'Exchange Greylist Configuration' in the Control Panel to configure Greylist Agent"

write-host "Registering agent" -f "green"
Install-TransportAgent -Name "Exchange Greylist Agent" -TransportAgentFactory "GreyListAgent.GreyListAgentFactory" -AssemblyPath "$EXDIR\GreylistAgent.dll"

write-host "Enabling agent" -f "green" 
enable-transportagent -Identity "Exchange Greylist Agent" 
set-transportagent "Exchange Greylist Agent" -Priority:2
get-transportagent 
 
write-host "Starting Edge Transport" -f "green" 
net start MSExchangeTransport 
 
write-host "Installation complete. Check previous outputs for any errors!" -f "yellow" 
