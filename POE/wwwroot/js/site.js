//Code Attribution
//Author:aelor
//Link: https://stackoverflow.com/questions/21727317/how-to-check-confirm-password-field-in-form-without-reloading-page
//Let the user know that the password fields dont match
$('#password, #confirm_password').on('keyup', function () {
    if ($('#password').val() == $('#confirm_password').val()) {
        $('#message').html('');
    } else {
        $('#message').html('Passwords do not match.');
    }       
});

//When the home button is clicked, the user is redirected to the home page, if they havent filled out the form
$('#toHome').on('click', function () {
    if ($('#moduleCode').val() == '' || $('#moduleName').val() == '' || $('#moduleCredits').val() == '' ||
        $('#moduleHours').val() == '') {
        $('#modCode').html('');
        $('#modName').html('');
        $('#modCredits').html('');
        $('#hours').html('');     

        window.location.href = "Index";
    }
});

//Redirects the user to the home page
$('#toHomeMods').on('click', function () {
        window.location.href = "Index";   
});

//Redirects the user to the home page when they have reached their module limit
$('#modLimit').on('click', function () {
    window.location.href = "Index";
});




