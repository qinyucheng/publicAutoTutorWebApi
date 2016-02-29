nameList=[
	"AMELIA:D1",
	"OLIVIA:D2",
	"EMILY:D3",
	"AVA:D4",
	"ISLA:D5",
	"JESSICA:D6",
	"POPPY:D7",
	"ISABELLA:D8",
	"SOPHIE:D9",
	"MIA:D10",
	"RUBY:D11",
	"LILY:D12",
	"GRACE:D13",
	"EVIE:D14",
	"SOPHIA:D15",
	"ELLA:D16",
	"SCARLETT:D17",
	"CHLOE:D18",
	"ISABELLE:D19",
	"FREYA:D20",
	"CHARLOTTE:D21",
	"SIENNA:D22",
	"DAISY:D23",
	"PHOEBE:D24",
	"MILLIE:D25",
	"EVA:D26",
	"ALICE:D27",
	"LUCY:D28",
	"FLORENCE:D29",
	"SOFIA:D30",
	"LAYLA:D31",
	"LOLA:D32",
	"HOLLY:D33",
	"IMOGEN:D34",
	"MOLLY:D35",
	"MATILDA:D36",
	"LILLY:D37",
	"ROSIE:D38",
	"ELIZABETH:D39",
	"ERIN:D40",
	"MAISIE:D41",
	"LEXI:D42",
	"ELLIE:D43",
	"HANNAH:D44",
	"EVELYN:D45",
	"ABIGAIL:D46",
	"ELSIE:D47",
	"SUMMER:D48",
	"MEGAN:D49",
	"JASMINE:D50",
	"MAYA:D51",
	"AMELIE:D52",
	"LACEY:D53",
	"WILLOW:D54",
	"EMMA:D55",
	"BELLA:D56",
	"ELEANOR:D57",
	"ESME:D58",
	"ELIZA:D59",
	"GEORGIA:D60",
	"HARRIET:D61",
	"GRACIE:D62",
	"ANNABELLE:D63",
	"EMILIA:D64",
	"AMBER:D65",
	"IVY:D66",
	"BROOKE:D67",
	"ROSE:D68",
	"ANNA:D69",
	"ZARA:D70",
	"LEAH:D71",
	"MOLLIE:D71",
	"MARTHA:D73",
	"FAITH:D74",
	"HOLLIE:D75",
	"AMY:D76",
	"BETHANY:D77",
	"VIOLET:D78",
	"KATIE:D79",
	"MARYAM:D80",
	"FRANCESCA:D81",
	"JULIA:D82",
	"MARIA:D83",
	"DARCEY:D84",
	"ISABEL:D85",
	"TILLY:D86",
	"MADDISON:D87",
	"VICTORIA:D88",
	"ISOBEL:D89",
	"NIAMH:D90",
	"SKYE:D91",
	"MADISON:D92",
	"DARCY:D93",
	"AISHA:D94",
	"BEATRICE:D95",
	"SARAH:D96",
	"ZOE:D97",
	"PAIGE:D98",
	"HEIDI:D99",

	"LYDIA:D100",
	"OLIVER:D101",
	"JACK:D102",
	"HARRY:D103",
	"JACOB:D104",
	"CHARLIE:D105",
	"THOMAS:D106",
	"OSCAR:D107",
	"WILLIAM:D108",
	"JAMES:D109",
	"GEORGE:D110",
	"ALFIE:D111",
	"JOSHUA:D112",
	"NOAH:D113",
	"ETHAN:D114",
	"MUHAMMAD:D115",
	"ARCHIE:D116",
	"LEO:D117",
	"HENRY:D118",
	"JOSEPH:D119",
	"SAMUEL:D120",
	"RILEY:D121",
	"DANIEL:D122",
	"MOHAMMED:D123",
	"ALEXANDER:D124",
	"MAX:D125",
	"LUCAS:D126",
	"MASON:D127",
	"LOGAN:D128",
	"ISAAC:D129",
	"BENJAMIN:D130",
	"DYLAN:D131",
	"JAKE:D132",
	"EDWARD:D133",
	"FINLEY:D134",
	"FREDDIE:D135",
	"HARRISON:D136",
	"TYLER:D137",
	"SEBASTIAN:D138",
	"ZACHARY:D139",
	"ADAM:D140",
	"THEO:D141",
	"JAYDEN:D142",
	"ARTHUR:D143",
	"TOBY:D144",
	"LUKE:D145",
	"LEWIS:D146",
	"MATTHEW:D147",
	"HARVEY:D148",
	"HARLEY:D149",
	"DAVID:D150",
	"RYAN:D151",
	"TOMMY:D152",
	"MICHAEL:D153",
	"REUBEN:D154",
	"NATHAN:D155",
	"BLAKE:D156",
	"MOHAMMAD:D157",
	"JENSON:D158",
	"BOBBY:D159",
	"LUCA:D160",
	"CHARLES:D161",
	"FRANKIE:D162",
	"DEXTER:D163",
	"KAI:D164",
	"ALEX:D165",
	"CONNOR:D166",
	"LIAM:D167",
	"JAMIE:D168",
	"ELIJAH:D169",
	"STANLEY:D170",
	"LOUIE:D171",
	"JUDE:D172",
	"CALLUM:D173",
	"HUGO:D174",
	"LEON:D175",
	"ELLIOT:D176",
	"LOUIS:D177",
	"THEODORE:D178",
	"GABRIEL:D179",
	"OLLIE:D180",
	"AARON:D181",
	"FREDERICK:D182",
	"EVAN:D183",
	"ELLIOTT:D184",
	"OWEN:D185",
	"TEDDY:D186",
	"FINLAY:D187",
	"CALEB:D188",
	"IBRAHIM:D189",
	"RONNIE:D190",
	"FELIX:D191",
	"AIDEN:D192",
	"CAMERON:D193",
	"AUSTIN:D194",
	"KIAN:D195",
	"RORY:D196",
	"SETH:D197",
	"ROBERT:D198",
	"ALBERT:D199",
	"SONNY:D200"
];

var nameListMap={};
var nameListArray;

$(document).ready(function(){
	generateTable();
	GetnameList();
	$("#selectName").bind("click",function(){
		document.getElementById('light').style.display='none';
		document.getElementById('fade').style.display='none';
		
	});
});
var SelectedNickName;
function generateTable(){
	for(var i=0;i<nameList.length/4;i++){
		trHTML = "<tr><td></td><td></td><td></td><td></td></tr>";
		$("#NickNameList").append(trHTML);
		$("td").mouseover(function () {
		    $(this).css("background-color", "yellow");
		});
		$("td").mouseout(function () {
		    $(this).css("background-color", "white");
		});
		$("td").bind("click", function () {
		
		    SelectedNickName = $(this).text();
		    var getSelectedNickNameID = nameListMap[SelectedNickName];
		    layer_close(getSelectedNickNameID, SelectedNickName);
           
		});
	}
}

function GetnameList(){
	var a=0;
	for(var i=0;i<=3;i++){    
        for(var j=1;j<nameList.length/4;j++){
			nameListArray=nameList[a].split(":");
			nameListMap[nameListArray[0]]=nameListArray[1];
			document.getElementById("NickNameList").rows[j].cells[i].innerText = nameListArray[0];
            a++;
         }
	}

	
}

function backToParentPage() {

    var SID = nameListMap[SelectedNickName];
    layer_close(SID, SelectedNickName);


}