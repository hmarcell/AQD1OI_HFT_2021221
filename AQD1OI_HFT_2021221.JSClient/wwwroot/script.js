let brands = [];
let connection;
getbranddata();
setupSignalR();
let brandIdToUpdate = -1;

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:7293/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BrandCreated", (user, message) => {
        getbranddata();
    });
    connection.on("BrandDeleted", (user, message) => {
        getbranddata();
    });
    connection.on("BrandUpdated", (user, message) => {
        getbranddata();
    });
    connection.on("RentalCreated", (user, message) => {
        getbranddata();
    });
    connection.on("RentalDeleted", (user, message) => {
        getbranddata();
    });
    connection.on("RentalUpdated", (user, message) => {
        getbranddata();
    });
    connection.on("BikeCreated", (user, message) => {
        getbranddata();
    });
    connection.on("BikeDeleted", (user, message) => {
        getbranddata();
    });
    connection.on("BikeUpdated", (user, message) => {
        getbranddata();
    });

    connection.onclose(async () => {
            await start();
        });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};



function showbrands() {
    document.getElementById('bikes').style.display = 'none';
    document.getElementById('rentals').style.display = 'none';
    document.getElementById('brands').style.display = 'contents';
}
function showrentals() {
    document.getElementById('bikes').style.display = 'none';
    document.getElementById('brands').style.display = 'none';
    document.getElementById('rentals').style.display = 'contents';
}
function showbikes() {
    document.getElementById('rentals').style.display = 'none';
    document.getElementById('brands').style.display = 'none';
    document.getElementById('bikes').style.display = 'contents';
}

async function getbranddata() {
    await fetch('http://localhost:7293/brand')
        .then(x => x.json())
        .then(y => {
            brands = y;
            display();
        });
}





function display() {
    document.getElementById('brandresultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById("brandresultarea").innerHTML
            += "<tr><td>" + t.id + "</td><td>"
            + t.name
            + "</td><td>"
            + `<button type="button" onclick="brandremove(${t.id})">Delete</button>`
            + `<button type="button" onclick="brandshowupdate(${t.id})">Update</button>`
            + "</td></tr>";
        console.log(t.name);
    });
}

function brandremove(id) {;
    fetch('http://localhost:7293/brand/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function brandcreate() {
    let brandname = document.getElementById('brandname').value;
    fetch('http://localhost:7293/brand', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: brandname }
        ),
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function brandupdate() {
    let brandname = document.getElementById('brandnametoupdate').value;
    fetch('http://localhost:7293/brand', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: brandname, id: brandIdToUpdate }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbranddata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('brandupdateformdiv').style.display = 'none';

}

function brandshowupdate(id) {
    document.getElementById('brandnametoupdate').value = brands.find(t => t['id'] == id)['name'];
    document.getElementById('brandupdateformdiv').style.display = 'flex';
    brandIdToUpdate = id;
}