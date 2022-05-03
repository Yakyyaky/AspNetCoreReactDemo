import classNames from 'classnames'
import React from 'react'
import css from './TextInput.module.scss'

type props = {
  classNames?: string

} & React.InputHTMLAttributes<HTMLInputElement>

export default function TextInput(props: React.PropsWithChildren<props>) {
  return <input className={classNames(css.input, props.className)} {...props} />
}
