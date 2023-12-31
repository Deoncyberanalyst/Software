import bookListData from '../data/bookListData.json'
import mountainData from '../data/mountainData.json'
import customerData from '../data/customerData.json'

function selectJson(jsonFileName){

  var jsonFiles = [bookListData, mountainData, customerData]

  for (var i=0; i < jsonFiles.length; i++) {
    if (jsonFiles[i].name.toUpperCase() == jsonFileName.toUpperCase()) {
      return jsonFiles[i]
  }}

  //default return
  return null
}

function JsonToList(jsonFileName, data) {

  var json = selectJson(jsonFileName)

  if (json != null ) {
    json = json[data]
    var dataList = [];
    for (let i=0; i < json.length; i++) dataList.push(" " + json[i] + ",")
    return dataList;
  }
  else {
    return "No data for request."
  }
 }


export default {JsonToList};
