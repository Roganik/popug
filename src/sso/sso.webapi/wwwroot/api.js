const JwtStore = {
    get: () => { return localStorage.getItem("jwt") },
    set: (jwt) => { localStorage.setItem("jwt", jwt) },
    clear: () => { localStorage.removeItem("jwt") },
}

const ApiClient = {
    post: (url, body, onSuccess, onError) => {
        
        let request = {
            contentType: 'application/json',
            data: body,
            dataType: 'json',
            success: onSuccess,
            error: onError,
            processData: false,
            type: 'POST',
            url: url,
        }
        
        let jwt = JwtStore.get();
        if (jwt) {
            let authHeader = {
                headers: {
                    Authorization: 'Bearer ' + jwt,
                }
            }
            request = {...request, ...authHeader}
        }
        
        $.ajax(request);
    }
}