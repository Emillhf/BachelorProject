import Read_file

encoded_symbols = {
    'B': 'P',
    'b': 'p',
    '#': 'k',
    'H': 'h',
    '0': 'O',
    '1': 'I',
    'S': 'Z',
    's': 'z',
    'M': 'W',
    'm': 'w',
    '!': '!',
    '$': '$',
    'k': 'K'
}
   

def to_pointer(rules):
    updated_rules = []
    for rule in rules:
        elms = Read_file.extract_elms_tape(rule)
        elms[1], elms[2] = encoded_symbols[elms[1]], encoded_symbols[elms[2]]
        updated_rules.append(updated_rules)
        
        