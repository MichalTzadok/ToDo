const uri = '/users';
let users = [];
const token = localStorage.getItem("token");
var myHeaders = new Headers(); 
function getItems() {
    var headers = new Headers();
    headers.append("Authorization", "Bearer " + token);
    headers.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'GET',
        headers: headers,
        redirect: 'follow'
    };

    fetch(uri,requestOptions)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
        
}
function addItem() {
    const addIsAdminCheckbox = document.getElementById('add-isAdmin');
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');

    const item = {
        isAdmin: addIsAdminCheckbox.checked,
        name: addNameTextbox.value.trim(),
        password:addPasswordTextbox.value.trim()
    };
    var headers = new Headers();
    headers.append("Authorization", "Bearer " + token);
    headers.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'POST',
        headers: headers,
        redirect: 'follow',
        body: JSON.stringify(item)

    };
    
    fetch(uri, requestOptions)
        .then(response => 
            {   jumpToLogin(response.status)
                return response.json()
            })
        .then(() => {
            getItems();
            addIsAdminCheckbox.checked=false
            addNameTextbox.value = ''
            addPasswordTextbox.value=''
        })
        .catch(error => console.error('Unable to add item.', error));
}
function deleteItem(id) {
    var headers = new Headers();
    headers.append("Authorization", "Bearer " + token);
    headers.append("Content-Type", "application/json");
        fetch(`${uri}/${id}`, {
                method: 'DELETE',
                headers:headers,

            })
            .then(response => {
                if (!jumpToLogin(response.status))
                    getItems();
            }).catch(error => console.error('Unable to delete item.', error));
    }

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'user' : 'user kinds';
    document.getElementById('counter').innerText = `${itemCount} ${name}`;

}
function _displayItems(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isAdminCheckbox = document.createElement('input');
        isAdminCheckbox.type = 'checkbox';
        isAdminCheckbox.disabled = true;
        isAdminCheckbox.checked = item.isAdmin;

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isAdminCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);
        let td3 = tr.insertCell(2);
        let passwordNode = document.createTextNode(item.password);
        td3.appendChild(passwordNode);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    users = data;
}
function jumpToLogin(status) {
    if (status === 401) {
        alert("your token got expired,please login ")
        localStorage.setItem('token', "");
        location.href = "../index.html";
        return true;
    }
    return false;
}