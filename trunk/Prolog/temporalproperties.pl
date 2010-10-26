/*
Модуль содержащий определения свойств сети и её элементов через
темпоральную логику.
Часть комплекса программ 3Pv
*/

:- module(temporallogicproperties,
	[
		liveness/0
	]).

/* Запросы с темпоральной логикой. */


/*Живость сети*/
liveness:-
	init(_),
	transition(T),
	not(implies(init,all(pot(enable(T)))),M),!,
	fail.
liveness.

/*Живость перехода*/
live(T):-
	init(S),
	implies(init, all(pot(enable(T))), S).

/*Мёртвость перехода*/
dead(T):- init(S),implies(init,all(not(enable(T))),S).

/*Мёртвость сети*/
deadness:-
	init(S),
	transition(T),
	not(implies(init,all(not(enable(T)))),S),
	!,fail.
deadness.

/*Потенциальная живость перехода*/
potlive(T):-init(S),implies(init,pot(all(pot(enable(T)))),S).

/*Потенциальная живость сети*/
potliveness:-
	init(S),
	transition(_),
	not(implies(init,pot(all(pot(enable(t))))),S),
	!,fail.
potliveness.

/*Детерминированность срабатывания перехода*/
determ(T):-
	init(S),
	implies(init,all(implies(enable(T),pretilda(fired(T)))),S).
