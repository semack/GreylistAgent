GreyListAgent
==================

> To work correctly you must build the solution with references to 
> the correct Microsoft Exchange DLLs.

An Exchange Transport Protocol agent that implements greylisting

GreyListing works by temporarily refusing delivery of an email for a short period of time.
Once a server re-tries the same email a second time, it will be added to the list and no longer delayed.

https://en.wikipedia.org/wiki/Greylisting


- James DeVincentis
- james@hexhost.net

Usage:
-----
1. Build from source or download pre-compiled DLLS from here: https://kill-9.me/543/exchange-greylist-transport-agent

2. Copy the compiled .dll file to C:\CustomAgents\GreyListAgent.dll

3. Create a folder in the same directory as the DLL called GreyListAgentData.

4. Copy GreyListConfig.xml to the GreyListAgentData directory. Configure as necessary. Configuration options are fairly self explanitory.

5. Install the agent and set its priority so it is above everything except the Transport Rules filter.
  ```
  Install-TransportAgent -Name "Greylist Agent" -TransportAgentFactory:GreyListAgent.GreyListAgentFactory -AssemblyPath:"C:\CustomAgents\GreyListAgent.dll"
  Get-TransportAgent
  Set-TransportAgent "Greylist Agent" -Priority:2
  Enable-TransportAgent "Greylist Agent"
  restart-service msexchangetransport
  ```
  
6. Use.
