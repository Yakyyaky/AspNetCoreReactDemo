import React, { ButtonHTMLAttributes } from 'react'
import css from './Button.module.scss'

type props = {
  classNames?: string
} & React.ButtonHTMLAttributes<HTMLButtonElement>

export default function Button(props: React.PropsWithChildren<props>) {
  return <button className={css.button} {...props} />
}
