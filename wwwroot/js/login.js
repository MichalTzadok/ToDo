const uri = '/login';

function withoutLogin() {
    if (localStorage.getItem("token") != undefined && sessionStorage.getItem("changeUser") == undefined && localStorage.getItem("token") != "")
        location.href = "./html/task.html";
}
function toConnect(){
    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();
    login(name,password);
}

function login(name,password) {
    localStorage.clear();
    var headers = new Headers();
    // const name = document.getElementById('name').value.trim();
    // const password = document.getElementById('password').value.trim();

    headers.append("Content-Type", "application/json");
    var raw = JSON.stringify({
        Name: name,
        Password: password
    })
    var request = {
        method: "POST",
        headers: headers,
        body: raw,
        redirect: "follow",
    };

    fetch(uri, request)
        .then((response) => response.text())
        .then((result) => {
            if (result.includes("401")) {
                name.value = "";
                password.value = "";
                alert("user not exist!!")
            } else {
                token = result;
                localStorage.setItem("name", name);
                localStorage.setItem("password", password);
                localStorage.setItem("token", token)
                location.href = "./html/task.html";

            }
        }).catch((error) => alert("error", error));

}
function onSuccess(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userId = decodedToken.sub;
        console.log(userId);
        var userName = decodedToken.name;
        console.log(userName);

        login(userName, userId);
    } else {
        alert('Google Sign-In failed.');
    }
}
function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}