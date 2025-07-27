#    Welcome to the Parliament's brand new HR Management System

## More powered than ever!

Following from the work in my last two attempts I've made some minor improvements and added a feature that I had no time to finish on the last attempt.

The changes are as follows:
* Added an `AvatarService` that fetches the randomly generated avatars (this time using the [DiceBear collection Personaas](https://www.dicebear.com/styles/personas/)) and caches them for further use, so that the avatars are not constantly in a weird state of randomly changing! 
* Added an Admin panel with the ability to change two admin items, Departments and PayBands, both with a small amount of validation and with changes immediatelly reflected in the UI. Thanks to some solid architecture design on the first attempt I never had to made any change to the repo at all and the Service and Controller changes were also minimal! 

### Notes on running tests
To run the new Jasmine/Karma tests you need to `cd` into `UkParliament.CodeTest.Web/ClientApp` and manually run the `npm run test` command.  
This will open a new browser where the tests should appear and automatically run.