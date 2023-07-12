# PulseChainValidatorRewardsMonitor

This application will let you use your existing Twilio account to send you an SMS every time (a) your validator rewards wallet balance increases, and (b) every time you propose a new block.  The application is dotnet 7.0 and requires that you have the dotnet 7.0 sdk installed. 

To install dotnet 7.0 SDK you can run the following command:<br>
sudo apt-get update
<br>
sudo apt-get install -y dotnet-sdk-7.0
<br>
<br>

INSTRUCTIONS:
<br>
<br>
git clone https://github.com/Jerkistan/PulseChainValidatorRewardsMonitor.git
<br>
cd PulseChainValidatorRewardsMonitor/ValidatorRewardsMonitor
<br>
dotnet build ValidatorRewardsMonitor.csproj
<br>
cd bin/Debug/net7.0
<br>
dotnet ValidatorRewardsMonitor.dll /home/yourUserNameHere
<br>
<br>
Please note that the required parameter (/home/yourUserNameHere) is to specify a location in which the configuration of the application should reside.  Upon running the application for the first time, you will be prompted for some values to build out the configuration file.  The next time the application runs, you will not be required to build a configuration file.  Every time you run the application you will need to provide the directory in which the appsettings.json file resides.  For example, dotnet ValidatorRewardsMonitor.dll /home/jerkistan
<br>

If you would like to thank me for my time, please send me some PLS to 0x5eff71d47c9cc8e384f6e6fb72058e12fe1507a6 :)
