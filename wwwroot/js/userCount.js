// Create Connection

var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

//Connect to method that invoke aka recived notification from hub

connectionUserCount.on("updateTotalVies", (value) => {
    var newCountSpan = document.getElementById("totalViewCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newuserCount = document.getElementById("totalUserCounter");
    newuserCount.innerText = value.toString();
})

function newWindowLoadedOnClient() {
    connectionUserCount.send("NewWindowLoaded");
};
function fulfilled() {
    console.log("Success");
    newWindowLoadedOnClient();
};
function rejected() {
    console.log("Fail!");
}

connectionUserCount.start().then(fulfilled, rejected);