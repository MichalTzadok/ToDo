const uri = '/ToDo';
let tasks = [];
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
        const addNameTextbox = document.getElementById('add-name');
        const item = {
            isDone: false,
            name: addNameTextbox.value.trim()
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
            .then(response => response.json())
            .then(() => {
                getItems();
                addNameTextbox.value = '';
            })
            .catch(error => console.error('Unable to add item.', error));
    }
    

    function updateItem() {
            const itemId = document.getElementById('edit-id').value;
            const item = {
                id: parseInt(itemId, 10),
                isDone: document.getElementById('edit-isDone').checked,
                name: document.getElementById('edit-name').value.trim()
            };
            var headers = new Headers();
            headers.append("Authorization", "Bearer " + token);
            headers.append("Content-Type", "application/json");
            fetch(`${uri}/${itemId}`, {
                    method: 'PUT',
                    headers:headers,
                    body: JSON.stringify(item)
                })
                .then(() => getItems())
                .catch(error => console.error('Unable to update item.', error));
        
            closeInput();
            return false;
        }
        function deleteItem(id) {
            var headers = new Headers();
            headers.append("Authorization", "Bearer " + token);
            headers.append("Content-Type", "application/json");
                fetch(`${uri}/${id}`, {
                        method: 'DELETE',
                        headers:headers,

                    })
                    .then(() => getItems())
                    .catch(error => console.error('Unable to delete item.', error));
            }
            
        function closeInput() {
                document.getElementById('editForm').style.display = 'none';
            }
function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}
function displayEditForm(id) {
        const item = tasks.find(item => item.id === id);
    
        document.getElementById('edit-name').value = item.name;
        document.getElementById('edit-id').value = item.id;
        document.getElementById('edit-isDone').checked = item.isDone;
        document.getElementById('editForm').style.display = 'block';
    }
function _displayItems(data) {
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isDoneCheckbox = document.createElement('input');
        isDoneCheckbox.type = 'checkbox';
        isDoneCheckbox.disabled = true;
        isDoneCheckbox.checked = item.isDone;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isDoneCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasks = data;
}