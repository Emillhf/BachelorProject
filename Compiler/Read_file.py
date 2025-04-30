
def read_file(input_filename):
    file = open(input_filename, 'r')
    lines = file.readlines()
    lines = [line.strip() for line in lines]
    return lines

def extract_elms_tape(rule):
    rule = rule.replace('(','')
    rule = rule.replace(')','')
    rule = rule.split(',')
    return rule

def write_file_newline(output_filename, data):
    outfile = open(output_filename, 'w+')
    for line in data:
        outfile.write(line + "\n")