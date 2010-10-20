/*
	Ìîäóëü ñîäåðæàùèé îïðåäåëåíèÿ ñâîéñòâ ñåòè è å¸ ýëåìåíòîâ.
	×àñòü êîìïëåêñà ïðîãðàìì 3Pv
*/

:- module(report,
	[
		make_report/0
	]).

body:-write('<?xml version="1.0" encoding="iso-8859-1"?>'),nl,
	write('<report text="Îò÷¸ò">'),nl,
	content,
	write('</report>').

content:-tab(3),write('<net text="Ñåòü: èìÿ='),netname(N),write(N),write('; òèï='),nettype(T),write(T),write(';">'),nl,
	places,
	transitions,
	net_properties,
	tab(3),write('</net>'),nl,
	tab(3),write('<statesspace text="Ïðîñòðàíñòâî ñîñòîÿíèé:">'),nl,
	states,
	arcs,
	tab(3),write('</statesspace>'),nl.

places:-place(P),
	tab(6),write('<place id="'),write(P),write('">'),nl,
	tab(9),write('<k_restrict text="K-îãðàíè÷åííîñòü:">'),nl,
	tab(12),write('<value>'),assertz(max(0)),k_restrict(P),max(K),retract(max(_)),write(K),write('</value>'),nl,
	tab(9),write('</k_restrict>'),nl,
	tab(6),write('</place>'),nl,fail;true.


/* âûâîäèò âñå ïåðåõîäû ñåòè */

transitions:-
	transition(T),
	tab(6),write('<transition id="'),write(T),write('">'),nl,
	tab(9),write('<properties text="Ñâîéñòâà:">'),nl,
	% tab(12),write('<live text="Æèâîñòü ïåðåõîäà:">'),nl,
	% tab(15),write('<value>'),live_p(T),write('</value>'),nl,
	% tab(12),write('</live>'),nl,
	% tab(12),write('<potlive text="Ïîòåíöèàëüíàÿ æèâîñòü ïåðåõîäà:">'),nl,
	% tab(15),write('<value>'),potlive_p(T),write('</value>'),nl,
	% tab(12),write('</potlive>'),nl,
	tab(12),write('<dead text="Ì¸ðòâîñòü ïåðåõîäà:">'),nl,
	tab(15),write('<value>'),dead_p(T),write('</value>'),nl,
	tab(12),write('</dead>'),nl,
	% tab(12),write('<determ text="Äåòåðìèíèðîâàííîñòü ïåðåõîäà:">'),nl,
	% tab(15),write('<value>'),determ_p(T),write('</value>'),nl,
	% tab(12),write('</determ>'),nl,
	tab(9),write('</properties>'),nl,
	tab(6),write('</transition>'),nl,fail;true.


/* âûâîäèò âñå ñâîéñòâà õàðàêòåðíûå äëÿ ñåòè âöåëîì */

net_properties:-
	tab(6),write('<properties text="Ñâîéñòâà:">'),nl,
	tab(9),write('<deadlock text="Íàëè÷èå ãëîáàëüíîãî òóïèêà:">'),nl,
	tab(12),write('<value>'),deadlock_p,write('</value>'),nl,
	tab(9),write('</deadlock>'),nl,
	tab(9),write('<liveness text="Æèâîñòü:">'),nl,
	tab(12),write('<value>'),liveness_p,write('</value>'),nl,
	tab(9),write('</liveness>'),nl,
	tab(9),write('<potliveness text="Ïîòåíöèàëüíàÿ æèâîñòü:">'),nl,
	tab(12),write('<value>'),potliveness_p,write('</value>'),nl,
	tab(9),write('</potliveness>'),nl,
	tab(9),write('<deadness text="Ì¸ðòâîñòü:">'),nl,
	tab(12),write('<value>'),deadness_p,write('</value>'),nl,
	tab(9),write('</deadness>'),nl,
	tab(6),write('</properties>'),nl.


/*Êëîçû îá¸ðòêè íàä îäíîèì¸ííûìè ñ ïîñòôèêñîì _p*/

live_p(T):-live(T),write('true').
live_p(_):-write('false').

dead_p(T):-dead(T),write('true').
dead_p(_):-write('false').

deadlock_p:-deadlock,write('true').
deadlock_p:-write('false').

liveness_p:-liveness,write('true').
liveness_p:-write('false').

deadness_p:-deadness,write('true').
deadness_p:-write('false').

potlive_p(T):-potlive(T),write('true').
potlive_p(_):-write('false').

potliveness_p:-potliveness,write('true').
potliveness_p:-write('false').

determ_p(T):-determ(T),write('true').
determ_p(_):-write('false').

dls_p(N):-dls(N),write('true'),!.
dls_p(_):-write('false').

/* âûâîäèò âñå ñîñòîÿíèÿ*/

states:-rstate(N,S),
	tab(6),write('<state id="'),write('S'),write(N),write('">'),nl,
	tab(9),write('<value>'),write(S),write('</value>'),nl,
	tab(9),write('<properties text="Ñâîéñòâà:">'),nl,
	tab(12),write('<deadlock text="Òóïèê:">'),nl,
	tab(15),write('<value>'),dls_p(N),write('</value>'),nl,
	tab(12),write('</deadlock>'),nl,
	tab(9),write('</properties>'),nl,
	tab(6),write('</state>'),nl,fail;true.
	

/* âûâîäèò âñå ïåðåõîäû ìåæäó ñîñòîÿíèÿìè*/

arcs:-
	gds(S1,T,S2),
	tab(6),write('<arc source="'),write('S'),write(S1),write('" target="'),write('S'),write(S2),write('">'),nl,
	tab(9),write('<value>'),write(T),write('</value>'),nl,
	tab(6),write('</arc>'),nl,fail;true.
	
make_report:-rebuildall,open(report,write,RP),tell(RP),body,tell(user),close(RP).