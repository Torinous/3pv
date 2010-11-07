/*
Модуль построения пространства состояний модели.
Часть комплекса программ 3Pv
*/

:- module(statespace,
	[
		createStateSpace/0,
		clearStateSpace/0,
		stateSpaceToDotFormatTmpFile/1,
		gds/3,
		rstate/2,
		remove/3,
		insert/3
	]).

:-use_module(properties).

:- dynamic rstate/2.
:- dynamic gds/3.
:- dynamic count/1.
:- dynamic gds_backup/3.
:- dynamic defaultNodeShape/1.
:- dynamic defaultEdgeLength/1.
:- dynamic useMarkingInStateLabel/0.

% стартовый клоз построения пространства состояний
createStateSpace:-
	clearStateSpace,
	init(S),
	asserta(rstate(0,S)),
	assertz(count(0)), !,
	seq(S,_,_),
	fail;true.

% Удаление баз данных:
clearStateSpace:-
	retractall(rstate(_,_)),
	retractall(gds(_,_,_)),
	retractall(count(_)).

seq(M,[T|L],M1):-
	arc(M,T,M2),
	rstate(NM,M),
	inbase(NM,T,M2),
	seq(M2,L,M1).
seq(M,[],M).

inbase(NM,T,M1):-
	rstate(NM1,M),
	remove(M,M1,[]),!,
	assertz(gds(NM,T,NM1)),
	fail.
inbase(NM,T,M1):-
	retract(count(N)),
	NM1 is N + 1,
	assertz(count(NM1)),
	assertz(rstate(NM1,M1)),
	write(NM),tab(1),
	assertz(gds(NM,T,NM1)).

% Предикаты работы со списками
% (в основном используются в кодировке PrT-сети в предикатах типа arc)

remove([E|X],L2,L3):-
	delel(E,L2,LP),
	remove(X,LP,L3).
remove([],L,L).

delel(X,[X|L],L).
delel(X,[Y|L],[Y|L1]):-
	delel(X,L,L1).

insert([],L,L).
insert([X|L1],L2,[X|L3]):-
	insert(L1,L2,L3).

inlist(X,[X|_]).
inlist(X,[_|L]):-
	inlist(X,L).


/*процедура удаляющая из базы дублирующиеся переходы.
ниженаписанное следует считать хаком код требует рефакторинга и исправления
*/
%% так будет до тех пор, пока не кончатся факты ds(_,_,_)
backupall:-
	gds(X,Y,Z),
	retract(gds(X,Y,Z)),
	ds_backup(X,Y,Z),
	fail;true. %% нет фактов - нет проблемы

ds_backup(X,Y,Z):-
	backuponce(X,Y,Z),!.

%% не будем повторять чужих ошибок: если факт уже есть, то всё
backuponce(X,Y,Z):-
	gds(X,Y,Z);
	assertz(gds_backup(X,Y,Z)). %% а если нет, то добавим единожды

restoreall:-
	gds_backup(X,Y,Z),
	retract(gds_backup(X,Y,Z)),
	assertz(gds(X,Y,Z)),
	fail;true.

rebuildall:-
	backupall,
	restoreall.


/*	Трансляция пространства состояний в dot-формат	*/

getDefaultNodeShape(Shape):-
	defaultNodeShape(Shape);
	Shape = rectangle.

getDefaultEdgeLength(Length):-
	defaultEdgeLength(Length);
	Length = 3.

stateSpaceToDotFormatTmpFile(FileName):-
	tmp_file_stream(text, FileName, Stream),
	tell(Stream),
	stateSpaceToDotFormat,
	tell(user),
	close(Stream).

stateSpaceToDotFormat:-write('digraph net{'),nl,
	paramsForDotFormat,
	statesForDotFormat,
	arcsForDotFormat,
	write('}').

paramsForDotFormat:-
	getDefaultNodeShape(Shape),
	getDefaultEdgeLength(Length),
	tab(3),write('size="25,25";'),nl,
	tab(3),write('node [shape='),write(Shape),write(', style = filled];'),nl,
	tab(3),write('edge [len='),write(Length),write('];'),nl.

stateLabel(Number,TokenList,Label):-
	useMarkingInStateLabel,
	atom_concat('S',Number,Label),
	atom_concat(Label,TokenList,Label).

stateLabel(Number,_,Label):-atom_concat('S',Number,Label).

stateColor(_,TokenList,',color=green'):-init(S),TokenList==S.
stateColor(Number,_,',color=red'):-deadlock(Number).
stateColor(_,_,'').

statesForDotFormat:-
	rstate(Number,TokenList),
	tab(3),write('S'),write(Number),
	write('['),
	write('label="'),stateLabel(Number,TokenList,Label),write(Label),write('"'),
	stateColor(Number,TokenList,ColorString),write(ColorString),
	write(']'),nl,
	fail;true.

arcsForDotFormat:-
	gds(S1,T,S2),
	tab(3),write('S'),write(S1),write(' -> '),write('S'),write(S2),write('[label="'),write(T),write('"]'),nl,
	fail;true.























