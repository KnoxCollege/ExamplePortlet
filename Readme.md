# JX.Net Example Portlet
## Target Purpose
The example portlet contained here is a simple portlet that makes use IEmailAddressService and IEmailAddressTypeLookup from the JX Common Webservices project to provide examples of how to integrate the framework into a JICS Portlet

## Usage
Usage of the framework in JICS portlets is comparatively straightforward once the setup work is done.  All Lookups and Services are instantiated via structuremap.  Otherwise, portlet development remains largely unchanged.

## Notes
* An encryption key is defined in the web.config in the appSettings section: \<add key="JXCredentialsKey" value="SomeValueHere" /> 
* Base URL's and username and passwords are stored in the FWK_ConfigSettings table in the ICS.NET Database with category of 'CustomJX' These include: [Module].Password (Encrypted), [Module].Username, [Module].Url, and IgnoreSSLErrors
* Logging for JX can be enabled in the web.config
```
<appender name="JXFramework" type="log4net.Appender.RollingFileAppender">
  <file value="C:\\Public\\jxframework.txt" />
  <appendToFile value="true" />
  <maximumFileSize value="10000KB" />
  <rollingStyle value="Size" />
  <layout type="log4net.Layout.PatternLayout">
	<conversionPattern value="%d{HH:mm:ss} [%t] %-5p %c - %m%n" />
  </layout>
</appender>
<logger name="Jenzabar.JX">
  <level value="INFO" />
  <appender-ref ref="JXFramework" />
</logger>
```
