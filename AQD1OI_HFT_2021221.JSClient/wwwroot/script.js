let brands = [];
let bikes = [];
let rentals = [];
let connection;
getbranddata();
getbikedata();
getrentaldata();
setupSignalR();
let brandIdToUpdate = -1;
let bikeIdToUpdate = -1;
let rentalIdToUpdate = -1;

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
        getrentaldata();
    });
    connection.on("RentalDeleted", (user, message) => {
        getrentaldata();
    });
    connection.on("RentalUpdated", (user, message) => {
        getrentaldata();
    });
    connection.on("BikeCreated", (user, message) => {
        getbikedata();
    });
    connection.on("BikeDeleted", (user, message) => {
        getbikedata();
    });
    connection.on("BikeUpdated", (user, message) => {
        getbikedata();
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
            branddisplay();
        });
}

async function getbikedata() {
    await fetch('http://localhost:7293/bike')
        .then(x => x.json())
        .then(y => {
            bikes = y;
            bikedisplay();
        });
}

async function getrentaldata() {
    await fetch('http://localhost:7293/rental')
        .then(x => x.json())
        .then(y => {
            rentals = y;
            rentaldisplay();
        });
}





function branddisplay() {
    document.getElementById('brandresultarea').innerHTML = "";
    brands.forEach(t => {
        document.getElementById("brandresultarea").innerHTML
            += "<tr><td>" + t.id + "</td><td>"
            + t.name
            + "</td><td>"
            + `<button type="button" onclick="brandremove(${t.id})">Delete</button>`
            + `<button type="button" onclick="brandshowupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function bikedisplay() {
    document.getElementById('bikeresultarea').innerHTML = "";
    bikes.forEach(t => {
        document.getElementById("bikeresultarea").innerHTML
            += "<tr><td>" + t.id + "</td><td>"
            + t.model
            + "</td><td>"
            + t.price
            + "</td><td>"
            + t.brandID
            + "</td><td>"
            + `<button type="button" onclick="bikeremove(${t.id})">Delete</button>`
            + `<button type="button" onclick="bikeshowupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function rentaldisplay() {
    document.getElementById('rentalresultarea').innerHTML = "";
    rentals.forEach(t => {
        document.getElementById("rentalresultarea").innerHTML
            += "<tr><td>" + t.id + "</td><td>"
            + t.renter
            + "</td><td>"
            + t.bikeID
            + "</td><td>"
            + t.date
            + "</td><td>"
            + `<button type="button" onclick="rentalremove(${t.id})">Delete</button>`
            + `<button type="button" onclick="rentalshowupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function brandremove(id) {
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

function bikeremove(id) {
    fetch('http://localhost:7293/bike/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbikedata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function rentalremove(id) {
    fetch('http://localhost:7293/rental/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getrentaldata();
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

function bikecreate() {
    let bikemodel = document.getElementById('bikemodel').value;
    let bikeprice = document.getElementById('bikeprice').value;
    let bikebrandid = document.getElementById('bikebrandid').value;
    fetch('http://localhost:7293/bike', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                model: bikemodel,
                price: bikeprice,
                brandID: bikebrandid
            }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbikedata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function rentalcreate() {
    let renter = document.getElementById('rentalrenter').value;
    let bikeid = document.getElementById('rentalbikeid').value;
    let date = document.getElementById('rentaldate').value;
    fetch('http://localhost:7293/rental', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                renter: renter,
                bikeID: bikeid,
                date: date
            }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getrentaldata();
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

function bikeupdate() {
    let bikemodel = document.getElementById('bikemodeltoupdate').value;
    let bikeprice = document.getElementById('bikepricetoupdate').value;
    let bikebrandid = document.getElementById('bikebrandIDtoupdate').value;
    fetch('http://localhost:7293/bike', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                model: bikemodel,
                price: bikeprice,
                brandID: bikebrandid,
                id: bikeIdToUpdate
            }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getbikedata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('bikeupdateformdiv').style.display = 'none';

}

function rentalupdate() {
    let renter = document.getElementById('rentalrentertoupdate').value;
    let bikeid = document.getElementById('rentalbikeIDtoupdate').value;
    let date = document.getElementById('rentaldatetoupdate').value;
    fetch('http://localhost:7293/rental', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                renter: renter,
                date: date,
                bikeID: bikeid,
                id: rentalIdToUpdate
            }
        ),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getrentaldata();
        })
        .catch((error) => { console.error('Error:', error); });
    document.getElementById('rentalupdateformdiv').style.display = 'none';

}

function brandshowupdate(id) {
    document.getElementById('brandnametoupdate').value = brands.find(t => t['id'] == id)['name'];
    document.getElementById('brandupdateformdiv').style.display = 'flex';
    brandIdToUpdate = id;
}

function bikeshowupdate(id) {
    let bike = bikes.find(x => x['id'] == id);
    document.getElementById('bikemodeltoupdate').value = bike['model'];
    document.getElementById('bikepricetoupdate').value = bike['price'];
    document.getElementById('bikebrandIDtoupdate').value = bike['brandID'];
    document.getElementById('bikeupdateformdiv').style.display = 'flex';
    bikeIdToUpdate = id;
}

function rentalshowupdate(id) {
    let rental = rentals.find(x => x['id'] == id);
    document.getElementById('rentalrentertoupdate').value = rental['renter'];
    document.getElementById('rentaldatetoupdate').value = rental['date'];
    document.getElementById('rentalbikeIDtoupdate').value = rental['bikeID'];
    document.getElementById('rentalupdateformdiv').style.display = 'flex';
    rentalIdToUpdate = id;
}