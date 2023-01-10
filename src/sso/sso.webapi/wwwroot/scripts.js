$("#login-btn").click(() => {
    let login = $("#login-input").val();
    let body = JSON.stringify({ login: login });
    
    let handleResponse = (data) => {
        $("#login-result").addClass("show");
        $("#login-response").text(JSON.stringify(data,false, 2));
    }

    ApiClient.post('/login', body, 
        (data, textStatus, jqXHR) => { 
            handleResponse(data);
            JwtStore.set(data.jwt);
            }, 
        (jqXHR, textStatus, errorThrown) => { handleResponse(textStatus); }
    );
})