const uri = '/login';

function withoutLogin() {
    if (localStorage.getItem("token") != undefined && sessionStorage.getItem("changeUser") == undefined && localStorage.getItem("token") != "")
        location.href = "./html/task.html";
}


function login() {
    var headers = new Headers();
    const name = document.getElementById('name').value.trim();
    const password = document.getElementById('password').value.trim();

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