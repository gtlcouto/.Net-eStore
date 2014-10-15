
            // global scoped variables
            var orderDate;
            var oldRow;
            var oldClass;
            var xml;
            var popupTbl;
            var oldElem
            var noteNo = 0;
            var collNotes;
            var myWindow;
            var ext = 0.00;
            
            function cur(num) {
                num = num.toString().replace(/\$|\,/g, '');
                if (isNaN(num))
                    num = "0";
                sign = (num == (num = Math.abs(num)));
                num = Math.floor(num * 100 + 0.50000000001);
                cents = num % 100;
                num = Math.floor(num / 100).toString();
                if (cents < 10)
                    cents = "0" + cents;
                for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                    num = num.substring(0, num.length - (4 * i + 3)) + ',' +
                         num.substring(num.length - (4 * i + 3));
                return (((sign) ? '' : '-') + '$' + num + '.' + cents);
            }

            $(document).ready(function () {

                $('td').click(function () { // click on a table cell in notes
                    if ($(this).attr('id') == 'OrderNo') {  // if we have more than 1 table, we want the right one
                        var id = '#jqPopup'; // empty table
                        //Get the window height and width
                        var winH = $(window).height();
                        var winW = $(window).width();
                        //Set the popup window to center
                        $(id).css('top', winH / 2 - $(id).height() / 2);
                        $(id).css('left', winW / 2 - $(id).width() / 2);
                        $(id).show();

                        if (oldRow != null) { // reset old row to previous state
                            oldRow.removeAttr("style");
                            oldRow.removeAttr("class");
                            $(oldRow).toggleClass("itemSelected");
                            $(oldRow).toggleClass(oldClass);
                       }
                       
                       oldRow = $(this).parents("tr");
                       oldClass = oldRow.attr("class");
                       oldRow.removeAttr("style");
                       oldRow.removeAttr("class");
                       oldRow.toggleClass("itemSelected");             
  
                        cellContents = $(this).html();
                        noteNo = parseInt(cellContents.substr(cellContents.indexOf(" ") + 1, cellContents.length - (cellContents.indexOf(" ") + 1)));  // note number

                        if (jQuery.browser.msie) {   // Internet Explorer
                            xmlDocument = new ActiveXObject("Microsoft.XMLDOM");
                            xmlDocument.async = "false";
                            xmlDocument.loadXML(NotesXML);
                            collNotes = xmlDocument.selectNodes("//note");              // all note nodes IE
                        } else {
                            var parser = new DOMParser();
                            xmlDocument = parser.parseFromString(NotesXML, "text/xml");
                            collNotes = xmlDocument.getElementsByTagName("note");       // all note nodes FF
                        }

                        popupBuilder();
                    }
                }); // td click

                // click on [X] anchor to close popup
                $('a').click(function (e) {
                    //Cancel the link behavior
                    e.preventDefault();
                    $('#dialog, .popupWindow').hide();
                });

            });

            function popupBuilder() {
                var tbl = $('<table>');
                // delete rows from old table (if it exists)
                $(tbl).find("tr").remove();

                /* Row */
                var tr = $("<tr>");
                var td = $("<td colspan='5'>").text("Note # " + noteNo);
                td.addClass("tblItemBigHead");
                td.appendTo(tr);       // add cell to table
                tr.appendTo(tbl);      // add row to table
                /* Row */
                tr = $("<tr>");
                td = $("<td>").text("From");
                td.addClass("tblItemHead");
                td.css({ 'width': '20%', 'text-align': 'center', 'font-weight': 'bold' });
                td.appendTo(tr);       // add cell to table
                td = $("<td>").text("To");
                td.addClass("tblItemHead");
                td.css({ 'width': '15%', 'text-align': 'center' });
                td.appendTo(tr);       // add cell to table
                td = $("<td>").text("Topic");
                td.addClass("tblItemHead");
                td.css({ 'width': '15%', 'text-align': 'center' });
                td.appendTo(tr);       // add cell to table
                td = $("<td>").text("Message");
                td.addClass("tblItemHead");
                td.css({ 'width': '35%', 'text-align': 'center' });
                td.appendTo(tr);       // add cell to table
                td = $("<td>").text("Date");
                td.addClass("tblItemHead");
                td.css({ 'width': '15%', 'text-align': 'center' });
                td.appendTo(tr);       // add cell to table
                tr.appendTo(tbl);      // add row to table

                for (i = 0; i < collNotes.length; i++) {
                    // select correct note  
                    tr = $("<tr>").addClass("itemData");

                    if (jQuery.browser.msie) {   // Internet Explorer

                        if (noteNo == collNotes.item(i).selectSingleNode("id").text) {
                            td = $("<td>").css({ 'width': '20%', 'text-align': 'left' });
                            td.text(collNotes.item(i).selectSingleNode("from").text);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '15%', 'text-align': 'left' });
                            td.text(collNotes.item(i).selectSingleNode("to").text);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '15%', 'text-align': 'left' });
                            td.text(collNotes.item(i).selectSingleNode("heading").text);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '35%', 'text-align': 'left' });
                            td.text(collNotes.item(i).selectSingleNode("notebody").text);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '15%', 'text-align': 'left' });
                            td.text(collNotes.item(i).selectSingleNode("date").text);
                            td.appendTo(tr);       // add cell to table
                        }
                    } else {  // firefox or chrome
                        if (noteNo == collNotes[i].childNodes[0].firstChild.nodeValue) { // id
                            td = $("<td>").css({ 'width': '20%' });
                            td.text(collNotes[i].childNodes[1].firstChild.nodeValue);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '15%', 'text-align': 'center' });
                            td.text(collNotes[i].childNodes[2].firstChild.nodeValue);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '15%', 'text-align': 'center' });
                            td.text(collNotes[i].childNodes[3].firstChild.nodeValue);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '35%', 'text-align': 'center' });
                            td.text(collNotes[i].childNodes[4].firstChild.nodeValue);
                            td.appendTo(tr);       // add cell to table
                            td = $("<td>").css({ 'width': '15%', 'text-align': 'center' });
                            td.text(collNotes[i].childNodes[5].firstChild.nodeValue);
                            td.appendTo(tr);       // add cell to table
                        }  // end if
                    } // end if

                    tr.appendTo(tbl);      // add row to table
                }   // end for

                $('#popupTbl').find("tr").remove();      // clean out old stuff
                $('#popupTbl').append($(tbl).html());    // put in new stuff
            }
  
