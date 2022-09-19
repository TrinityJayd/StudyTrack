# Study Track App<a name="TOP"></a>

This is a solution to Part 1 the PROG6212 POE.
### Note: You will first need to build the solution, to allow some components to be displayed correctly.

## Table of contents

- [Overview](#overview)
  - [The scenario](#the-scenario)
  - [Built with](#built-with)
  - [Screenshots](#screenshots)
  - [Extra Features](#extra-features)


## Overview

## The scenario

The scenario is to create an application that can be used for personal
study tracking. 
The user will have to input:
- Module Code
- Module Name
- Module Credits
- Class Hours
- Semester Start Date
- Weeks In The Semester

The program will calculate the number of hours a student needs to study **WEEKLY** using the module credits and that number multiplied by ten will be the number of
hours spent on it throughout the semester. For instance, PROG6212 is 15 credits, so 150 hours
must be spent on it. Some of that will be class hours, and the rest will have to be distributed
throughout the weeks.

When the user decides to log how long they have studied for they will be prompted to enter the: 
 - Module Code
 - Time they studied for
 - Date they last studied
 
Whenever the user adds a module it will displayed on a Datagrid to them.
When they update a module it will be displayed on a Datagrid along with the:
 - Hours they should study for
 - Time they studied for
 - The time left they need to study for
 - The date they last studied

The program also makes use of a custom class library that deals with all module calculations as well as validation.

### Built With
  
  - C#
  - WPF(Windows Presentation Foundation)
  ## Images
  - [Undraw](https://undraw.co/) 
  - [Flaticon](https://www.flaticon.com/free-icon/books-stack-of-three_29302?term=books&page=1&position=7&page=1&position=7&related_id=29302&origin=search)

### Screenshots

On starting the app the user will see a SyncFusion License Message because the program makes use of a SyncFusion package. Simply click OK to continue the program.
- - - -


<img src="SolutionImages/SyncFusionLicense.png" alt="SyncFusion License" width="800" height="600" />


- - - -

After clicking OK, the user will see the landing page.

- - - -


<img src="SolutionImages/LandingPage.png" alt="Landing page" width="800" height="600" />


- - - -

If they click get started they will be directed to the add modules page.

- - - -


<img src="SolutionImages/GettingStarted.png" alt="Add modules page" width="800" height="600" />


- - - -

If the user does not input a certain field, an error will be displayed.

- - - -


<img src="SolutionImages/MissingData.png" alt="Missing Data Error" width="800" height="600" />


- - - -

If the user has already added the module, an error will be displayed.

- - - -


<img src="SolutionImages/ModuleExists.png" alt="Module Exists" width="800" height="600" />


- - - -

Module codes/ names are only allowed to have uppercase or lowercase letters, digits, and optional underscores or spaces. If they user tries to input any other character, an error will be displayed.

- - - -


<img src="SolutionImages/ErrorFormat.png" alt="Format Error" width="800" height="600" />


- - - -

If the user clicks the complete button before saving the module, an alert will be displayed.

- - - -


<img src="SolutionImages/ContinueWithoutSave.png" alt="Alert the user that the module was not saved" width="800" height="600" />


- - - -

When the user successfully adds a module, all fields will be cleared, and any inputs regarding the semester details will be hidden.

- - - -


<img src="SolutionImages/AddData.png" alt="Add modules page" width="800" height="600" />


- - - -

Once the user inputs all data correctly and clicks on the Add Module button, a Complete button will become visible.If the user clicks on the complete button they will be redirected to the home page or they can fill in details to add another module.

- - - -


<img src="SolutionImages/Home.png" alt="Home page" width="800" height="600" />


- - - -

On the home page, if the user clicks on the View Modules button a page will be displayed that shows them a list of all their modules.

- - - -


<img src="SolutionImages/ModuleList.png" alt="Module list displayed to user" width="800" height="600" />


- - - -

If they click the Delete Module button on the home page, a page will be displayed that asks the user to select the module they want to delete and confirm the deletion of the module.

- - - -


<img src="SolutionImages/DeleteModule.png" alt="Delet modules page" width="800" height="600" />


- - - -

If they click on the Complete Button without confirming the deletion or selecting a module, an error will be displayed.

- - - -


<img src="SolutionImages/DeleteError.png" alt="Delete error without choosing module" width="800" height="600" />

<img src="SolutionImages/DeleteModuleChoose.png" alt="Delete error without confirming deletion" width="800" height="600" />


- - - -

If they choose a module and confirm the deletion, the module will be deleted and removed as an option.

- - - -


<img src="SolutionImages/DeletedModule.png" alt="Shows that the module was deleted" width="800" height="600" />


- - - -

On the home page, if the user clicks the Record Hours button they will be redirected to a page to record the time that they have been studying for and if the user clicks the complete button without filling in all the details, they will not be able to move further. **Note: The dates the user can choose from are the start date of the current semester and the current date that they are using the application.**

- - - -


<img src="SolutionImages/RecordHours.png" alt="Record hours page" width="800" height="600" />
 
<img src="SolutionImages/RecordHoursError.png" alt="Error if user does not complete all inputs" width="800" height="600" />
 

- - - - 

The user can also view the modules, along with the calculate study hours, amount of time they spent studying and the date they last studied, when they click the View Study Hours button.

- - - -


<img src="SolutionImages/StudyHours1.png" alt="Datagrid of study hours details" width="800" height="600" />

<img src="SolutionImages/StudyHours2.png" alt="Datagrid of study hours details" width="800" height="600" />


- - - - 

If the user clicks the Home button before Getting Started, no modules will be added, therefore the datagrid where they can view all modules and the datagrid with the study hour details will not be filled.

- - - -

<img src="SolutionImages/HoverImage.png" alt="On Hover of the home button" width="800" height="600" />

<img src="SolutionImages/NoModulesList.png" alt="Datagrid for modules details empty" width="800" height="600" />

<img src="SolutionImages/NoModulesStudyHours.png" alt="Datagrid for study hours details empty" width="800" height="600" />


- - - -

### Extra Features
  
  Some extra features in this app include:
  - A delet function that allows user to delete any modules they no longer need to study for.
  - The user is able to order the study hours datagrid, by calculate field **Study Hours Per Week**.

[Go To TOP](#TOP)
