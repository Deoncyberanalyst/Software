import { useState } from 'react'


export default () => {
  const [count, setCount] = useState(0)

  return (
    <button className="outline" onClick={()=> (count +1)}>Increase support {count}</button>

  )

}