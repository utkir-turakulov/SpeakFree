
$('.switchery').click(() => {
    console.log("checkbox clicked");
});

function clicked() {
    var status = $('#is_anonymous_check');

    if (status) {
        $('#is_anonymous_check').checked = false;
    } else {
        $('#is_anonymous_check').checked = true;
    }
    
    console.log("clicked: ");
}

function fillData(Id) {

    var title = $('#edit_message_modal input[Name="Title"]');
    var text = $('#edit_message_modal textarea[Name="Text"]');
    var anonymousCheck = $('#edit_message_modal #is_anonymous_check'); 
    var messageId = $('#edit_message_modal input[Name="Id"]');
    var message = {};

    $.get("Get/" + Id)
        .done(function (data) {
            message = data;
            $(title).val(message.title);
            $(text).val(message.text);
            if (!message.isAnonymous) {
                $(anonymousCheck).click();
            }
            messageId.val(message.id);
        })
        .fail(function (error) {
            console.log(error.statusText);
        });
}


function fillOnDelete(mes_id) {
    var id = $('#delete-message-modal input[name="messageId"]').val(mes_id);

    //$(id).val(mes_id);
}