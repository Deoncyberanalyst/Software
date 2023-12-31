//git pull --all && npm start
//$ git pull --all && npm start
// hands-on-react@0.0.0 start
//vite
import React, { Component } from 'react'
import Welcome from './components/Welcome'
import Support from './components/Support'
import ViewData from './components/ViewData'

function App() {
  
  return (
    <hgroup>
      <Welcome name="to me!"/>
      <ViewData toolName="Mountain Data" data="highest peaks"/>
      <ViewData toolName="Mountain Data" data="lowest points"/>
      <ViewData toolName="Customer Data" data="name"/>
      <ViewData toolName="Customer Data" data="customers"/>

      <ViewData toolName="Book data" data="name"/>
      <ViewData toolName="Book data" data="books"/>

      <ViewData toolName="Test Data" data="lowest points"/>
      <ViewData toolName="new data" data="customers"/>
      <button className="outline" onClick={()=> alert("Hi there")}>Click me</button>

     <Support />
    </hgroup>
  )
}
export default App
