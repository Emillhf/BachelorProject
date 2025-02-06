open System.IO

type Replace = char*char
type Move = string

type Operation = Replace of Replace | Move of Move
type Rule = string*Operation*string

let read_file(filename:string) =
    let lines = File.ReadAllLines(filename)
    let hashtag = Array.findIndex(fun elm -> elm = "#") lines
    let pi = Array.findIndex(fun elm -> elm = "π") lines

    let rules = lines[0..hashtag-1] 
    let states = lines[hashtag+1..pi-1]
    let input = lines[pi+1..][0]
    (rules,states,input)