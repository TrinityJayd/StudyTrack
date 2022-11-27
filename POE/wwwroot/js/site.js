//Code Attribution
//Author:aelor
//Link: https://stackoverflow.com/questions/21727317/how-to-check-confirm-password-field-in-form-without-reloading-page
$('#password, #confirm_password').on('keyup', function () {
    if ($('#password').val() == $('#confirm_password').val()) {
        $('#message').html('');
    } else {
        $('#message').html('Passwords do not match.');
    }       
});


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


