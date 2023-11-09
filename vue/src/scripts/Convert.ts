export class Convert {
  public static KebabCase(str: string): string {
    const hyphenateRE = /([^-])([A-Z])/g
    return str.replace(hyphenateRE, '$1-$2').replace(hyphenateRE, '$1-$2').toLowerCase()
  }

  public static CamelCase(name: string): string {
    const SPECIAL_CHARS_REGEXP = /([:\-_]+(.))/g
    const MOZ_HACK_REGEXP = /^moz([A-Z])/
    return name
      .replace(SPECIAL_CHARS_REGEXP, function (_, separator, letter, offset) {
        return offset ? letter.toUpperCase() : letter
      })
      .replace(MOZ_HACK_REGEXP, 'Moz$1')
  }
}
