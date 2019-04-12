/*
 * Botton change
 */
var abajo = "images//abajo.png";
var derecha = "images//derecha.png";
var months = ["JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER"];
var days = ["SATURDAY", "SUNDAY", "MONDAY", "TUESDEY", "WEDNESDAY", "THURSDAY", "FRIDAY"];
var motnhModifier = 0;
var today = new Date();

// Funcionalidad Java script
loadCalendar(today);
/*
 * Show
 */ 
function showEmail(num) {
    if ($(".email[meta-email='" + num + "'] .date img").attr("src") === abajo) {
        $(".email[meta-email='" + num + "'] .filaGrande").hide();
        document.querySelector(".email[meta-email='" + num +"'] .date > img").src = derecha;
    } else {
        $(".email[meta-email='" + num + "'] .filaGrande").show();
        document.querySelector(".email[meta-email='" + num +"'] .date > img").src = abajo;
    }
}


/*
 * Delete
 */
function removeEmail(num) {
    document.querySelector(".email[meta-email='" + num + "']").remove();

    remaining();
}

function remaining() {
    var msn = document.querySelectorAll(".delete").length;
    document.querySelector("#remain").innerText = msn + " Previously Messages";

}
/*
 * Calendar
 */
document.querySelector("#imgIzquierdaCal").addEventListener("click", function () {
    monthChange(new Date(today.getFullYear(), today.getMonth() + --motnhModifier));
});

document.querySelector("#imgDerechaCal").addEventListener("click", function () {
    monthChange(new Date(today.getFullYear(), today.getMonth() + ++motnhModifier));
});

function loadCalendar(fecha){
	document.querySelector("#date").innerText = fecha.getDate() + " / " + months[fecha.getMonth()] + " / " + days[fecha.getDay()];
	monthChange(fecha);

}

function monthChange(fecha) {
    document.querySelector("#calDate").innerText = months[fecha.getMonth()] + " " + fecha.getFullYear();
    setCalDays();
}

function setCalDays() {
    // *daysInMonth - https://medium.com/@nitinpatel_20236/challenge-of-building-a-calendar-with-pure-javascript-a86f1303267d
    var daysInMonth = 32 - new Date(today.getFullYear(), today.getMonth() + motnhModifier, 32).getDate();

    var daysMonth = document.querySelectorAll(".calCol > span");
    var dayModifier = new Date(today.getFullYear(), today.getMonth() + motnhModifier, 1).getDay();

    for (var i = 0; i < daysMonth.length; i++) {
        daysMonth[i].innerText = "";
    }

    for (var i = 0; i < daysInMonth; i++) {
        daysMonth[i + dayModifier].innerText = i + 1;
    }
}

/*
 * WEB API
 */

var uri = "api/mywebapi/";

/*
 * Submenú
 */
$.post(uri + "GetCurrentUser")
    .done(function (data) {
        document.querySelector("#current_user_name").innerText = data.UserRS.Name + " " + data.UserRS.Surnames;
        document.querySelector("#current_user_img").src = data.UserRS.Avatar;
    });


// Puntuació
$.post(uri + "GetPoints")
    .done(function (data) {
        // Li passem la informació dels punts del usuari dins la seva posició del html
        document.querySelector("#user_points").innerHTML = "Total Points " + data.PointsRS.Points;
    });

/*
// REMINDERS IN THE CALENDAR
$.post(uri + "GetReminders")
    .done(function (data) {
     // Li passem la informaci� dels punts del usuari dins la seva posici� del html
     document.querySelector("#user_points").innerHTML = "Total Points " + data.PointsRS.Points;
    });
    */

// Recordatoris del calendari
$(document).ready(
    function () {
        // Recordatoris del calendari
        $.post(uri + "GetReminders")
            .done(function (data) {

                // Amagam els blocks de la secci� del event
                $("#calInfoCliente, #calInfoZone, #calcInfoInfo").css("display", "none");

                // Si cliquem damunt un dia...
                $(".calCol").on("click", function () {

                    $(this).css({ "color": "white", "background-color": "#FF5B33", "border-radius": "800px" });

                    // Llevarem el display de tot el contingut del recordatori per poder ser visible
                    $("#calInfoCliente, #calInfoZone, #calcInfoInfo").css("display", "");

                    if (today == data.Memory.ReminderDate) {
                        // Passarem la informaci� que hi ha emmagatzemada a la base de dades, dins cada una de les seves posicions del html
                        document.querySelector("#description").innerHTML = data.Memory.Description;
                        document.querySelector("#reminder-date").innerHTML = data.Memory.ReminderDate;
                        document.querySelector("#title").innerHTML = data.Memory.Title;
                        document.querySelector("#address").innerHTML = data.Memory.Address;
                        document.querySelector("#phone-number").innerHTML = data.Memory.PhoneNumber;
                    }

                    // En clicar damunt la creu del recordatori, aquest es tancar�
                    $("#img-cruz").on("click", function () {
                        $("#calInfoZone, #calcInfoInfo").css("display", "none");
                    });
            });

        // Li canviem el color quan passem per damunt el dia 
        $(".calCol").hover(
            function () { $(this).css({ "color": "#33BBFF", "background-color": "#FF3374", "border-radius": "800px" }) },
            function () { $(this).css({ "color": "", "background-color": "" }) }
        );
    });
    });

/*
 * MESSAGES
 */ 
$.post(uri + "GetMessages")
    .done(function (data) {
        var count = 0;

        $.each(data.Messages, function (key, item) {

            document.querySelector("#messaging").appendChild(setEmail(count++, item));
            
        });

        document.querySelector("#messaging").appendChild(prevMessages());
    });

function setEmail(msgNum, item) {
    var email = document.createElement("div");
    email.setAttribute("class", "email");
    email.setAttribute("meta-email", msgNum);

    email.appendChild(setRow(item, msgNum));
    email.appendChild(setBigRow(msgNum, item));

    return email;
}

function setRow(item, msgNum) {
    var row = document.createElement("div");
    row.className = "fila";

    row.appendChild(setCheckBox());
    row.appendChild(setState(item.State));
    row.appendChild(setSender(item.Sender));
    row.appendChild(setSubject(item.Subject));
    row.appendChild(setDate(item.Date, msgNum));

    return row;
}

function setCheckBox() {
    var check = document.createElement("div");
    check.classList.add("checkBox");
    check.classList.add("col");
    check.classList.add("centrarTotal");

    var checkbox = document.createElement("input");
    checkbox.setAttribute("type", "checkbox");

    check.appendChild(checkbox);

    return check;
}

function setState(mState) {
    var state = document.createElement("div");
    state.classList.add("estado");
    state.classList.add("col");
    state.classList.add("centrarAlinear");

    var img = document.createElement("img");
    if (mState === 0) img.src = "images/unread.png"; else img.src = "images/readed.png"

    state.appendChild(img);

    return state;
}

function setSender(mSender) {
    var sender = document.createElement("div");
    sender.classList.add("sender");
    sender.classList.add("col");
    sender.classList.add("centrarAlinear");

    var span = document.createElement("span");
    span.className = "textSender";
    span.innerText = mSender.Name + " " + mSender.Surnames;

    sender.appendChild(span);

    return sender;
}

function setSubject(mSubject) {
    var subject = document.createElement("div");
    subject.classList.add("subject");
    subject.classList.add("col");
    subject.classList.add("centrarAlinear");

    var span = document.createElement("span");
    span.className = "textSubject";
    span.innerText = mSubject;

    subject.appendChild(span);

    return subject;
}

function setDate(mDate, msgNum) {
    var date = document.createElement("div");
    date.classList.add("date");
    date.classList.add("centrarAlinear");

    var div = document.createElement("div");
    var span = document.createElement("span");
    span.className = "textDate";
    var day = new Date(mDate)
    span.innerText = day.getDate() + " " + months[day.getMonth()].substring(0,1) + months[day.getMonth()].substring(1).toLowerCase() + " " + day.getFullYear();
    div.appendChild(span)

    var img = document.createElement("img");
    img.src = "images/derecha.png";
    img.setAttribute("onClick", "showEmail(" + msgNum + ")");

    date.appendChild(div)
    date.appendChild(img)

    return date
}

function setBigRow(msgNum, item) {

    var bigRow = document.createElement("div");
    bigRow.className = "filaGrande";

    bigRow.appendChild(setMsn(item));
    bigRow.appendChild(setButtons(msgNum));
    bigRow.appendChild(setClearBoth());

    return bigRow;
}

function setMsn(item) {
    var msn = document.createElement("div");
    msn.className = "msn";

    msn.appendChild(setMsnTitle(item.Subject));
    msn.appendChild(setContent(item.Content));

    return msn;
}

function setMsnTitle(mTitle) {
    var title = document.createElement("div");
    title.classList.add("subjRecuadro")
    title.classList.add("msn");
    title.classList.add("centrarAlinear");
    
    var h1 = document.createElement("h1");
    h1.innerText = mTitle;

    title.appendChild(h1);

    return title;
}

function setContent(mContent) {
    var content = document.createElement("p");
    content.className = "textMsn";
    content.innerText = mContent;

    return content;
}

function setButtons(msgNum) {
    var buttons = document.createElement("div");
    buttons.classList.add("botones");
    buttons.classList.add("centrarAlinear");

    buttons.appendChild(setReplay());
    buttons.appendChild(setDelete(msgNum));

    return buttons;
}

function setReplay() {
    var replay = document.createElement("input");
    replay.classList.add("replay");
    replay.classList.add("boton");
    replay.classList.add("centrarTotal");
    replay.setAttribute("type", "button");
    replay.setAttribute("value", "REPLAY");

    return replay;
}

function setDelete(msgNum) {
    var bDelete = document.createElement("input");
    bDelete.classList.add("delete");
    bDelete.classList.add("boton");
    bDelete.classList.add("centrarTotal");
    bDelete.setAttribute("meta-email", msgNum);
    bDelete.setAttribute("type", "button");
    bDelete.setAttribute("value", "DELETE");
    bDelete.setAttribute("onClick", "removeEmail(" + msgNum + ")");

    return bDelete;
}

function setClearBoth() {
    var clear = document.createElement("div");
    clear.className = "clear-both";

    return clear;
}

function prevMessages() {
    var fila = document.createElement("div");
    var previ = document.createElement("div");
    var remain = document.createElement("span");

    fila.className = "fila";
    previ.setAttribute("id", "previously");
    previ.className = "centrarTotal";
    remain.setAttribute("id", "remain");

    var msn = document.querySelectorAll(".delete").length;
    remain.innerText = msn + " Previously Messages";

    previ.appendChild(remain);
    fila.appendChild(previ);

    return fila;
}  