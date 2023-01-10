$("#login-btn").click(() => {
    let login = $("#login-input").val();
    let body = JSON.stringify({ login: login });
    
    let handleResponse = (data) => {
        $("#login-result").addClass("show");
        $("#login-response").text(JSON.stringify(data,false, 2));
    }

    $.ajax({
        contentType: 'application/json',
        data: body,
        dataType: 'json',
        success: function(data, textStatus, jqXHR) {
            handleResponse(data);
        },
        error: function(jqXHR, textStatus, errorThrown) {
            handleResponse(textStatus);
        },
        processData: false,
        type: 'POST',
        url: '/login'
    });
})