GreyListAgent [![Build Status](https://travis-ci.org/jmdevince/GreylistAgent.png?branch=master)](https://travis-ci.org/jmdevince/GreylistAgent)
==================

> To work correctly you must build the solution with references to 
> the correct Microsoft Exchange DLLs.

An Exchange Transport Protocol agent that implements greylisting

GreyListing works by temporarily refusing delivery of an email for a short period of time.
Once a server re-tries the same email a second time, it will be added to the list and no longer delayed.


Features
-----
1. Configurable clean up intervals for confirmed and unconfirmed entries
2. Configurable netmask application to deal with most mail clusters automatically
3. Whitelisting via rDNS (client) and IP address
4. Logging transactions for debugging purposes


Install Instructions:
-----
https://kill-9.me/543/exchange-greylist-transport-agent


Contact
-----
- James DeVincentis
- james@hexhost.net
