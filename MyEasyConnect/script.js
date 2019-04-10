/*
 * Botton change
 */
var abajo = "images//abajo.png";
var derecha = "images//derecha.png";
var months = ["JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER"];
var days = ["SATURDAY", "SUNDAY", "MONDAY", "TUESDEY", "WEDNESDAY", "THURSDAY", "FRIDAY"];
var motnhModifier = 0;
var today = new Date();

main();

function main(){
	remaining();
	loadCalendar(today);
	var btnDelete = document.querySelectorAll(".delete");

	for(var i = 0; i < btnDelete.length; i++){
		btnDelete[i].addEventListener("click", removeEmail);
	}

	document.querySelector("#imgIzquierdaCal").addEventListener("click", function(){
		monthChange(new Date(today.getFullYear(), today.getMonth() + --motnhModifier));
	});

	document.querySelector("#imgDerechaCal").addEventListener("click", function(){
		monthChange(new Date(today.getFullYear(), today.getMonth() + ++motnhModifier));
	});
}

$(document).ready(
	function(){
		
		$(".date > img").click(function(){
			if($(this).attr("src") === abajo){
				$(this).attr("src", derecha);
				$(this).parent().parent().siblings().hide()
			} else{
				$(this).attr("src", abajo);
				$(this).parent().parent().siblings().show()
			}
		});
	}
);

/*
 * Delete
 */
function removeEmail(){
	document.querySelector(".email[meta-email='" + this.getAttribute("meta-email") + "']").remove();
	
	remaining();
}

function remaining(){
	var msn = document.querySelectorAll(".delete").length;
	document.querySelector("#remain").innerText = msn;
}

/*
 * Calendar
 */
function loadCalendar(fecha){
	document.querySelector("#date").innerText = fecha.getDate() + " / " + months[fecha.getMonth()] + " / " + days[fecha.getDay()];
	monthChange(fecha);
}

function monthChange(fecha){
	document.querySelector("#calDate").innerText = months[fecha.getMonth()] + " " + fecha.getFullYear();
	setCalDays();
}

function setCalDays(){
	// *daysInMonth - https://medium.com/@nitinpatel_20236/challenge-of-building-a-calendar-with-pure-javascript-a86f1303267d
	var daysInMonth = 32 - new Date(today.getFullYear(), today.getMonth() + motnhModifier, 32).getDate();

	var daysMonth = document.querySelectorAll(".calCol > span");
	var dayModifier = new Date(today.getFullYear(), today.getMonth() + motnhModifier, 1).getDay();

	for(var i = 0; i < daysMonth.length; i++){
		daysMonth[i].innerText = "";
	}
	
	for(var i = 0; i < daysInMonth; i++){
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
        document.querySelector("#current_user_name").innerHTML = data.UserRS.Name + " " + data.UserRS.Subname;
        document.querySelector("#current_user_img").src = data.UserRS.Avatar;
    });

// POINTS
$.post(uri + "GetPoints")
    .done(function (data) {
        document.querySelector("#user_points").innerHTML = "Total Points " + data.PointsRS.Points;
    });