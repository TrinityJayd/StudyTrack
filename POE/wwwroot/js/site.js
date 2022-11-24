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
