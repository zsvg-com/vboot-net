var T=(C,r,i)=>new Promise((u,x)=>{var o=n=>{try{m(i.next(n))}catch(f){x(f)}},w=n=>{try{m(i.throw(n))}catch(f){x(f)}},m=n=>n.done?u(n.value):Promise.resolve(n.value).then(o,w);m((i=i.apply(C,r)).next())});import{A as M,b0 as E,ao as j,r as k,P as N,j as A,u as e,B as G,D as P,F as D,G as L,w as s,a6 as a,b1 as K,ae as d,J as c,aT as p,b2 as O,H as U,b3 as V,b4 as q,b5 as W,b6 as H,b7 as J,K as Q,b8 as X,ab as Y,b9 as Z,ba as ee}from"./vendor.8e08a5be.js";/* empty css              *//* empty css               *//* empty css               *//* empty css               */import{u as se,a as ae,L as y,_ as te,b as oe}from"./LoginFormTitle.946a8fd3.js";import{b as le,a as ne,f as re,h as ie}from"./index.1f50be75.js";const xe=M({setup(C){const r=Z,i=ee,u=E.Item,x=j.Password,{t:o}=le(),{notification:w,createErrorModal:m}=ie(),{prefixCls:n}=ne("login"),f=re(),{setLoginState:b,getLoginState:$}=se(),{getFormRules:B}=ae(),F=k(),S=k(!1),_=k(!1),g=N({account:"vben",password:"123456"}),{validForm:h}=oe(F),I=A(()=>e($)===y.LOGIN);function R(){return T(this,null,function*(){const v=yield h();if(!!v)try{S.value=!0;const t=yield f.login({password:v.password,username:v.account,mode:"none"});t&&w.success({message:o("sys.login.loginSuccessTitle"),description:`${o("sys.login.loginSuccessDesc")}: ${t.realName}`,duration:3})}catch(t){m({title:o("sys.api.errorTip"),content:t.message||o("sys.api.networkExceptionMsg"),getContainer:()=>document.body.querySelector(`.${n}`)||document.body})}finally{S.value=!1}})}return(v,t)=>(G(),P(Y,null,[D(s(te,{class:"enter-x"},null,512),[[L,e(I)]]),D(s(e(E),{class:"p-4 enter-x",model:e(g),rules:e(B),ref:(l,z)=>{z.formRef=l,F.value=l},onKeypress:X(R,["enter"])},{default:a(()=>[s(e(u),{name:"account",class:"enter-x"},{default:a(()=>[s(e(j),{size:"large",value:e(g).account,"onUpdate:value":t[0]||(t[0]=l=>e(g).account=l),placeholder:e(o)("sys.login.userName"),class:"fix-auto-fill"},null,8,["value","placeholder"])]),_:1}),s(e(u),{name:"password",class:"enter-x"},{default:a(()=>[s(e(x),{size:"large",visibilityToggle:"",value:e(g).password,"onUpdate:value":t[1]||(t[1]=l=>e(g).password=l),placeholder:e(o)("sys.login.password")},null,8,["value","placeholder"])]),_:1}),s(e(i),{class:"enter-x"},{default:a(()=>[s(e(r),{span:12},{default:a(()=>[s(e(u),null,{default:a(()=>[s(e(K),{checked:_.value,"onUpdate:checked":t[2]||(t[2]=l=>_.value=l),size:"small"},{default:a(()=>[d(c(e(o)("sys.login.rememberMe")),1)]),_:1},8,["checked"])]),_:1})]),_:1}),s(e(r),{span:12},{default:a(()=>[s(e(u),{style:{"text-align":"right"}},{default:a(()=>[s(e(p),{type:"link",size:"small",onClick:t[3]||(t[3]=l=>e(b)(e(y).RESET_PASSWORD))},{default:a(()=>[d(c(e(o)("sys.login.forgetPassword")),1)]),_:1})]),_:1})]),_:1})]),_:1}),s(e(u),{class:"enter-x"},{default:a(()=>[s(e(p),{type:"primary",size:"large",block:"",onClick:R,loading:S.value},{default:a(()=>[d(c(e(o)("sys.login.loginButton")),1)]),_:1},8,["loading"])]),_:1}),s(e(i),{class:"enter-x"},{default:a(()=>[s(e(r),{md:8,xs:24},{default:a(()=>[s(e(p),{block:"",onClick:t[4]||(t[4]=l=>e(b)(e(y).MOBILE))},{default:a(()=>[d(c(e(o)("sys.login.mobileSignInFormTitle")),1)]),_:1})]),_:1}),s(e(r),{md:8,xs:24,class:"!my-2 !md:my-0 xs:mx-0 md:mx-2"},{default:a(()=>[s(e(p),{block:"",onClick:t[5]||(t[5]=l=>e(b)(e(y).QR_CODE))},{default:a(()=>[d(c(e(o)("sys.login.qrSignInFormTitle")),1)]),_:1})]),_:1}),s(e(r),{md:7,xs:24},{default:a(()=>[s(e(p),{block:"",onClick:t[6]||(t[6]=l=>e(b)(e(y).REGISTER))},{default:a(()=>[d(c(e(o)("sys.login.registerButton")),1)]),_:1})]),_:1})]),_:1}),s(e(O),{class:"enter-x"},{default:a(()=>[d(c(e(o)("sys.login.otherSignIn")),1)]),_:1}),U("div",{class:Q(["flex justify-evenly enter-x",`${e(n)}-sign-in-way`])},[s(e(V)),s(e(q)),s(e(W)),s(e(H)),s(e(J))],2)]),_:1},8,["model","rules","onKeypress"]),[[L,e(I)]])],64))}});export{xe as default};