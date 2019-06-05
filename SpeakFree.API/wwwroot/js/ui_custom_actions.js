
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

