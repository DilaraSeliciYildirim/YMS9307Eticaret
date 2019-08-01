$(document).ready(function () {

    $("#js-add-member-form").validate({

        rules: {
            FirstName: "required",
            LastName:
            {
                required: true
       
            },
            Email: {
                required: true,
                email:true
            },
            Password: {
                required: true,
                rangelength: [8,16]
            },
            confirm: {
                equalTo:"#js-input-password"
            }

        }


    });

});