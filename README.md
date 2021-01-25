This is a small demo of how to use global action filters in an MVC or Razor Pages project to implement a lightweight query string "key" security approach. This can be appropriate for some API scenarios that don't require [jwt](https://jwt.io). This is akin to how Azure Function project security that uses a **code** query string argument to prevent anonymous use.

There are two `Filter` classes:
- [KeyActionFilter](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/KeyValidatorFilter/KeyActionFilter.cs) for MVC controllers
- [KeyPageFilter](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/KeyValidatorFilter/KeyPageFilter.cs) for Razor Pages
- They are both supported by a [static common class](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/KeyValidatorFilter/FilterCommon.cs) that provides the actual validation.

In the DemoApp, they are added at `Startup` for both [MVC](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/DemoApp/Startup.cs#L31) and [Razor Pages](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/DemoApp/Startup.cs#L36).

The "allowed keys" are defined in the config file [here](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/DemoApp/appsettings.json#L10-L14), access at runtime during startup [here](https://github.com/adamfoneil/KeyValidatorFilter/blob/master/DemoApp/Startup.cs#L41-L45)
