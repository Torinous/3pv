/*
������ ���������� ����������� ������� ���� � � ��������� �����
������������ ������.
����� ��������� �������� 3Pv
*/

:- module(temporallogicproperties,
	[
		liveness/0
	]).

/* ������� � ������������ �������. */


/*������� ����*/
liveness:-
	init(_),
	transition(T),
	not(implies(init,all(pot(enable(T)))),M),!,
	fail.
liveness.

/*������� ��������*/
live(T):-
	init(S),
	implies(init, all(pot(enable(T))), S).

/*̸������� ��������*/
dead(T):- init(S),implies(init,all(not(enable(T))),S).

/*̸������� ����*/
deadness:-
	init(S),
	transition(T),
	not(implies(init,all(not(enable(T)))),S),
	!,fail.
deadness.

/*������������� ������� ��������*/
potlive(T):-init(S),implies(init,pot(all(pot(enable(T)))),S).

/*������������� ������� ����*/
potliveness:-
	init(S),
	transition(_),
	not(implies(init,pot(all(pot(enable(t))))),S),
	!,fail.
potliveness.

/*������������������� ������������ ��������*/
determ(T):-
	init(S),
	implies(init,all(implies(enable(T),pretilda(fired(T)))),S).
