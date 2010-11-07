/*
	Модуль содержащий определения операторов темпоральной логики.
	Часть комплекса программ 3Pv
*/

:- module(temporallogic,
	[
		pre/3,
		non/1,
		not/2
	]).

:-use_module(statespace).

/* Операторы темпоральной логики */

pre(P,L,S):-arc(S,_,S1),
	P=..K,
	insert(K,[L,S1],P0),
	P01=..P0,
	call(P01),!.

outl(_,[]):-!.
outl(S,[S1|L]):-
	noneg(S,S1),
	outl(S,L).

noneg(S,S1):-remove(S,S1,[]),!,fail.
noneg(_,_).

pot(P1,P2,L,S):-outl(S,L),!,
	(conc(P2,S,P02),call(P02),
	!;conc(P1,S,P01),call(P01),
	pre(pot(P1,P2),[S|L],S)).
pot(P,S):-pot(true,P,[],S).

true(_).

inl(X,[Z|_]):-remove(X,Z,[]),!.
inl(X,[_|Y]):-inl(X,Y).

all(P,S):-all(true,P,[],S).
all(_,_,L,S):-
	inl(S,L),!.
all(P1,P2,L,S):-
	conc(P2,S,P02),
	call(P02),!,
	(pretilda(all(P1,P2),[S|L],S),!;
	conc(P1,S,P01),non(P01)).

pretilda(P,L,S):-non(cpretilda(P,L,S)).

cpretilda(P,L,S):-arc(S,_,S1),
	P=..K,
	insert(K,[L,S1],P0),
	P01=..P0,
	non(P01).

non(P):-call(P),!,fail;true.


inev(P,S):-inev(true,P,[],S).
inev(_,P2,L,S):-outl(S,L),
	conc(P2,S,P02),call(P02),
	!.
inev(P1,P2,L,S):-outl(S,L),
	arc(S,_,_),
	!,
	conc(P1,S,P01),call(P01),
	pretilda(inev(P1,P2),[S|L],S).

some(P,S):-some(true,P,[],S).
some(_,_,L,S):-inl(S,L),!.
some(P1,P2,L,S):-conc(P2,S,P02),call(P02),!,
	(pre(some(P1,P2),[S|L],S),!;
	conc(P1,S,P01),
	non(P01),
	!;non(arc(S,_,_))).
	
/*Невременные логические операторы*/

not(P,S):-conc(P,S,P01),call(P01),!,fail.
not(_,_).

or(P1,_,S):-conc(P1,S,P01),call(P01),!.
or(_,P2,S):-conc(P2,S,P02),call(P02).

and(P1,P2,S):-
	conc(P1,S,P01),
	call(P01),
	conc(P2,S,P02),
	call(P02).

implies(P1,P2,S):-
	conc(P1,S,P01),
	call(P01),
	not(P2,S),!,fail.
implies(_,_,_).

equ(P1,P2,S):-
	conc(P1,S,P01),
	call(P01),
	conc(P2,S,P02),
	call(P02),
	!.
equ(P1,P2,S):-
	not(P1,S),
	not(P2,S).

conc(P,S,P01):-
	P=..K,
	insert(K,[S],P0),
	P01=..P0.
