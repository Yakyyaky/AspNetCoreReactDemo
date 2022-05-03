import classNames from 'classnames'
import React, { useCallback } from 'react'
import css from './Toggle.module.scss'

interface Props {
  className?: string
  value: boolean
  onChange(value: boolean): void
  disabled?: boolean
}

export default function Toggle({ className, value, onChange, children, disabled }: React.PropsWithChildren<Props>) {
  const onClick = useCallback(() => {
    onChange && onChange(!value)
  }, [ value, onChange ])

  return <button disabled={disabled} className={classNames(css.toggle, className, value && css.checked)} onClick={onClick}>
    <div className={css.icon} />
    {children}
  </button>
}
