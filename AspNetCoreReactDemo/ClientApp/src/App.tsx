import React from 'react'
import logo from './logo.svg'
import css from './App.module.scss'
import AccountView from './components/AccountView'
import ApiPlaygroundView from './components/ApiPlaygroundView'

function App() {
  return (
    <div className={css.App}>
      <ApiPlaygroundView className={css.playground} />
      <AccountView className={css.account} />
    </div>
  )
}

export default App
