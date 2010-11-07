/*
Модуль содержащий определения свойств сети и её элементов.
Часть комплекса программ 3Pv
*/

:- module(properties,
	[
		enable/2,
		deadlock/1,
		k_restrict/1
	]).

:-use_module(statespace).
:-use_module(temporallogic).

:- dynamic max/1.

max(X,Y,Y) :- Y>X,!.
max(X,_,X).

/*Разрешён ли переход T при маркировке S*/
enable(T,S):-arc(S,T,_).

/*запрос является ли определённое состояние тупиком*/
deadlock(N):-non(gds(N,_,_)).

/*  Есть ли дублирующие маркировки? (no)*/
doubleMarking:- rstate(N1,M1),rstate(N2,M2),N1 \= N2,remove(M1,M2,[]).

/* Нет ли глобальных тупиков ?	(no)*/
deadlock:-rstate(N,_),non(gds(N,_,_)).

/* Есть ли инверсные маркировни (yes)*/
inverseMarking(M1,M2):-
	rstate(N1,M1),
	rstate(N2,M2),N1 \= N2,
	invlist(M1,M3),
	remove(M3,M2,[]).

/* Кол-во инверсных маркировок*/
i:-
	recons,assert(coim(0)),!,
	rstate(N1,M1),rstate(N2,M2),N1 \= N2,
	invlist(M1,M3),remove(M3,M2,[]),
	retract(coim(X)),X1 is X + 1,
	assert(coim(X1)),fail.

/* INVLIST*/

invlist([H|T],[H1|T1]):- invelem(H,H1),invlist(T,T1).
invlist([],[]).

invelem(X,X1):-functor(X,_,1),arg(1,X,A),inv(A,NA),argrep(X,1,NA,X1),!.
invelem(X,X1):-functor(X,_,2),arg(1,X,A),inv(A,NA),argrep(X,1,NA,X2),
					arg(2,X,B),inv(B,NB),argrep(X2,2,NB,X1).


/* Определение количества элементов в списке*/
num(_,0,[]).
num(E,N,[E|R]) :- num(E,N1,R), N is N1 + 1,!.
num(E,N,[_|R]) :- num(E,N,R).

/* Истинно если срабатыание перехода T ведёт в состояние S*/
fired(T,S):-arc(_,T,S).

/*вычисляет k-ограниченность позиции*/
k_restrict(P):-
	rstate(_,S),
	max(K),
	count(P,S,K1),
	max(K,K1,M),
	retract(max(_)),
	assertz(max(M)),
	fail;true.
%k_restrict(P,K):-rstate(1,S1),count(P,S1,K).


count(F,[H|L],C):-functor(H,Name,_),Name==F ->count(F,L,N),C is N+1;count(F,L,C).
count(_,[],C):-C is 0.




















