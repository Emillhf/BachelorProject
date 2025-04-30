module Interpreter
open System.IO
open Helper
open Types
let mutable flip = false
// let Mapping_t1 =
//    Map.empty. 
//       Add('B','P').
//       Add('b','p').
//       Add('#','k').
//       Add('1','I').
//       Add('0','O').
//       Add('S','Z').
//       Add('s','z').
//       Add('!','!').
//       Add('$','$').
//       Add('H','h').
//       Add('m','w').
//       Add('k','k').
//       Add('K','K').
//       Add('M','W');;
// let Mapping_t2 =
//    Map.empty. 
//       Add('B','P').
//       Add('b','p').
//       Add('p','p').
//       Add('k','k').
//       Add('K','K'). // K regel er manuelt tilføjet til URTMen
//       Add('#','#').
//       Add('1','I').
//       Add('!','!').
//       Add('$','$').
//       Add('0','O').
//       Add('S','Z').
//       Add('s','z').
//       Add('H','h').
//       Add('m','w').
//       Add('M','W');;

let RMT(rules:Map<int,list<Rule>>, (start1,final1):int*int, (input:tape,program:tape,states:tape)) =
    File.WriteAllText("3_tape_log.txt", "\n")
    let start = start1 //Starting state is always 1
    let final = final1 //Final state is always 0
    let mutable idx1 = 0
    let mutable idx2 = 0
    let mutable idx3 = 4
    let mutable current_state = start
    let mutable previous_state = -1

    let write1(symbol:char) = input[idx1] <- symbol
    let write2(symbol:char) = program[idx2] <- symbol
    let write3(symbol:char) = states[idx3] <- symbol

    let updateCurrentState (newState:int) = current_state <- newState

    let move1 (num:int) = idx1 <- idx1 + num
    let move2 (num:int) = idx2 <- idx2 + num
    let move3 (num:int) = idx3  <- idx3 + num
    
    let check (rule:Operation) =
        match rule with 
            | Move(_,_,_) -> true
            | Symbol(t1,t2,t3) -> 
                    fst t1 = input[idx1] && 
                    fst t2 = program[idx2]&& fst t3 = states[idx3]

    let act (rule:Rule) =
        // File.AppendAllText("3_tape_log.txt", rule.ToString() + "\n")
        // File.AppendAllText("3_tape_log.txt",    System.String(input) + ", " + string(input[idx1]) + "\n")
        // File.AppendAllText("3_tape_log.txt",    System.String(program[idx2-10..idx2+10]) + ", " + string(program[idx2]) + "\n")
        // File.AppendAllText("3_tape_log.txt",    System.String(states) + "\n")
        // File.AppendAllText("3_tape_log.txt", "idx: " + idx1.ToString() + "\n\n")
        // printfn "%A" (idx1, idx2, idx3)
        // printfn "%A" (rule)
        match second rule with 
            | Move(t1,t2,t3) -> 
                match t1 with
                    | "STAY" -> ()
                    | "RIGHT" -> move1(1)
                    | "LEFT" -> move1(-1)
                    | _-> failwith "Error when moving "
                match t2 with
                    | "STAY" -> ()
                    | "RIGHT" -> move2(1)
                    | "LEFT" -> move2(-1)
                    |_-> failwith "Error when moving"
                match t3 with               
                    | "STAY" -> ()
                    | "RIGHT" -> move3(1)
                    | "LEFT" -> move3(-1)
                    |_-> failwith "Error when moving"
            | Symbol(t1,t2,t3) -> 
                write1 (snd t1)
                write2 (snd t2)
                write3(snd t3) 

        updateCurrentState (third rule) //Update current_state
        if current_state = 169 then
                printfn "RESET"
                idx2 <- 0
                flip <- true
        // if flip then 
        //     File.AppendAllText("3_tape_log.txt",    string(rule) + "\n")
        //     File.AppendAllText("3_tape_log.txt",    System.String(input) + ", " + string(input[idx1]) + "\n")
        //     File.AppendAllText("3_tape_log.txt",    System.String(program[idx2..idx2+10]) + ", " + string(program[idx2]) + "\n")
        //     File.AppendAllText("3_tape_log.txt",    System.String(states) + "\n")
        //     File.AppendAllText("3_tape_log.txt", "idx: " + idx1.ToString() + "\n\n")

    let search (rules_list:Map<int,list<Rule>>) =
        let rec search_rec(rules_state: List<Rule>) = 
            match rules_state with
                | [rule:Rule] -> 
                    if check(second rule) then act rule
                | rule :: rest -> 
                    if check (second rule) then act rule
                    else search_rec rest
                | _ -> failwith "Shit wrong"

        search_rec(rules_list[current_state])

    while not(current_state = final) do
        if not(previous_state = current_state) then
            previous_state <- current_state
            search rules
        else 

            printfn "%A" rules[current_state]
            printfn "%A" (input[idx1], program[idx2], states[idx3])
            printfn "%A" (program[idx2-10..idx2+10])
            printfn "Current state: %A\nInput tape: %A, idx: %A\nProgram tape: %A, idx: %A\nState tape %A, idx: %A" current_state input idx1 program idx2 states idx3
            failwith "Rules are wrong in the above state"
    // printfn "%A" (idx1, idx2, idx3) 
    input, program, states