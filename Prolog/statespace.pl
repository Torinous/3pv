/*
Модуль построения пространства состояний модели.
Часть комплекса программ 3Pv
*/

:- module(statespace,
	  [
	   createStateSpace/0,
	   clearStateSpace/0,
	   stateSpaceToDotFormatTmpFile/1
	  ]).

:- dynamic rstate/2.
:- dynamic gds/3.
:- dynamic count/1.
:- dynamic gds_backup/3.

%стартовый клоз построения пространства состояний
createStateSpace:-
	clearStateSpace,
	init(S),
	asserta(rstate(0,S)),
	assertz(count(0)), !,
	seq(S,_,_),
	fail;true.

% Удаление баз данных:
clearStateSpace:-
	clearrs,
	cleargds,
	clearcount.

% удаление базы данных о состояниях
clearrs:-retract(rstate(_,_)),clearrs.
clearrs.

% удаление базы данных о дугах ГДС
cleargds:-retract(gds(_,_,_)),cleargds.
cleargds.

% удаление базы данных для счетчика достигнутых маркировок
clearcount:-retract(count(_)),clearcount.
clearcount.

% КОНСТРУКТОР ГДМ
seq(M,[T|L],M1):-
        arc(M,T,M2),
        rstate(NM,M),
        inbase(NM,T,M2),
        seq(M2,L,M1).
seq(M,[],M).

inbase(NM,T,M1):-
                  rstate(NM1,M),
                  remove(M,M1,[]),
                  !,
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

remove([E|X],L2,L3):-delel(E,L2,LP),remove(X,LP,L3).
remove([],L,L).

delel(X,[X|L],L).
delel(X,[Y|L],[Y|L1]):-delel(X,L,L1).

insert([],L,L).
insert([X|L1],L2,[X|L3]):-insert(L1,L2,L3).

inlist(X,[X|_]).
inlist(X,[_|L]):-inlist(X,L).



/*процедура удаляющая из базы дублирующиеся переходы.
ниженаписанное следует считать хаком код требует рефакторинга и исправления
*/
%% так будет до тех пор, пока не кончатся факты ds(_,_,_)
backupall:-
	gds(X,Y,Z),
	retract(gds(X,Y,Z)),
	ds_backup(X,Y,Z),
	fail;true. %% нет фактов - нет проблемы

ds_backup(X,Y,Z):-backuponce(X,Y,Z),!.

%% не будем повторять чужих ошибок: если факт уже есть, то всё
backuponce(X,Y,Z):-
	gds(X,Y,Z);
	assertz(gds_backup(X,Y,Z)). %% а если нет, то добавим единожды

restoreall:-
	gds_backup(X,Y,Z),
	retract(gds_backup(X,Y,Z)),
	assertz(gds(X,Y,Z)),
	fail;true.

rebuildall:-backupall,restoreall.

stateSpaceToDotFormatTmpFile(FileName):-
	tmp_file_stream(text, FileName, Stream),
	tell(Stream),
	stateSpaceToDotFormat,
	tell(user),
	close(Stream).

stateSpaceToDotFormat:-write('digraph net{'),nl,
	paramsForDotFormat,nl,
	statesForDotFormats,nl,
	arcsForDotFormats,nl,
	write('}').

paramsForDotFormat:-
	tab(3),write('size="20,20";'),nl,
	tab(3),write('node [shape = rectangle, style = filled];'),nl,
	tab(3),write('edge [len=3];'),nl.

statesForDotFormats:-
	rstate(Number,tokenList),
	tab(3),write('S'),write(Number),write('[label="'),
	write('S'),write(Number),write(tokenList),write('" red]'),nl,
	fail;true.

arcsForDotFormats:-
	gds(S1,T,S2),
	tab(3),write(S1),write(' -> '),write(S2),write('[label="'),write(T),write('"]'),nl,
	fail;true.














