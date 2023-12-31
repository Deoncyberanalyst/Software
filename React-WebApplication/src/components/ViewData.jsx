import RefData from '../services/RefData'

const ViewData = (params) => {

  const toolName = params.toolName
  const Data = RefData.JsonToList(params.toolName,params.data)

  return (
  <> 
  <h3>Data for {toolName}</h3>
  <p>{Data} </p>
  </>
  )
}

export default ViewData;