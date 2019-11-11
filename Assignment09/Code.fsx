(* 11.1.1 *)
let rec len xs =
    match xs with
    | [] -> 0
    | x::xr -> 1 + len xr;;

let rec lenc list cont =
    match list with
    | [] -> cont 0
    | head::tail -> lenCon tail (fun acc -> cont (acc + 1))

printf "The answer is ’%d’ \n" (lenc [2; 5; 7] id)

(* 11.1.2 *)
printf "The answer is ’%d’ \n" (lenc [2; 5; 7] (fun v -> 2*v))
(* It doubles the result, as the input function is no longer just the id function *)

(* 11.1.3 *)
let rec leni list acc =
    match list with
    | [] -> acc
    | head::tail -> leni tail (acc + 1)

(* They are both using a technique for achieving tail-recursion. One is using CPS, the other an accumulator *)

(* 11.2.1 *)
let rec rev xs =
    match xs with
    | [] -> []
    | x::xr -> rev xr @ [x]

let rec revc list cont =
    match list with
    | [] -> cont []
    | head::tail -> revc tail (fun acc -> cont (acc @ [head]))
    
printf "The answer is ’%A’ \n" (revc [1; 2; 3] id)

(* 11.2.2 *)
printf "The answer is ’%A’ \n" (revc [1; 2; 3] (fun v -> v @ v))
(* It concatenates the reversed list onto itself, due to the new input continuation. *)

(* 11.2.3 *)
let rec revi list acc =
    match list with
    | [] -> acc
    | head::tail -> revi tail ([head] @ acc)
    
(* 11.3 *)
let rec prod xs =
    match xs with
    | [] -> 1
    | x::xr -> x * prod xr

let rec prodc list cont =
    match list with
    | [] -> cont 1
    | head::tail -> prodc tail (fun acc -> cont (head * acc))
    
(* 11.4 *)
let rec prodcz list cont = (* prodc optimized to terminate if encountering zero *)
    match list with
    | [] -> cont 1
    | head::tail ->
        if head = 0
        then 0
        else prodcz tail (fun acc -> cont (head * acc))

let rec prodi list acc =
    match list with
    | [] -> acc
    | head::tail ->
        if head = 0
        then 0
        else prodi tail (acc * head)