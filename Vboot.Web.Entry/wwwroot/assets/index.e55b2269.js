var J=Object.defineProperty,O=Object.defineProperties;var L=Object.getOwnPropertyDescriptors;var C=Object.getOwnPropertySymbols;var I=Object.prototype.hasOwnProperty,W=Object.prototype.propertyIsEnumerable;var E=(d,n,e)=>n in d?J(d,n,{enumerable:!0,configurable:!0,writable:!0,value:e}):d[n]=e,z=(d,n)=>{for(var e in n||(n={}))I.call(n,e)&&E(d,e,n[e]);if(C)for(var e of C(n))W.call(n,e)&&E(d,e,n[e]);return d},P=(d,n)=>O(d,L(n));var F=(d,n,e)=>new Promise((B,_)=>{var u=i=>{try{m(e.next(i))}catch(p){_(p)}},h=i=>{try{m(e.throw(i))}catch(p){_(p)}},m=i=>i.done?B(i.value):Promise.resolve(i.value).then(u,h);m((e=e.apply(d,n)).next())});import{P as K}from"./index.4a6d20cf.js";import{u as Q,B as X,a as Y}from"./index.b3a760e0.js";import{ah as V}from"./index.1f50be75.js";import{h as Z}from"./edit.6fd2a5d5.js";import{A as R,P as D,a0 as v,B as A,a1 as j,a6 as l,w as r,H as w,J as T,u as a,a4 as U,bc as ee,r as k,bG as M,_ as H,ae as $}from"./vendor.8e08a5be.js";/* empty css               *//* empty css                */import"./useWindowSizeFn.2016176c.js";import"./useContentViewHeight.78015564.js";/* empty css               */const te=R({emits:["success","register"],setup(d,{emit:n}){const e=D({loadingText:"",url:"/bi/field/main",data:{},rules:{name:[{required:!0,message:"\u8BF7\u8F93\u5165\u540D\u79F0"}]}}),[B,{setDrawerProps:_,changeLoading:u,closeDrawer:h}]=Q(m=>F(this,null,function*(){e.loadingText="\u52A0\u8F7D\u4E2D",u(!0),m.record.id?e.data=yield V.get({url:e.url+"/one/"+m.record.id}):(e.data=m.record,e.data.avtag=!0),u(!1)}));return(m,i)=>{const p=v("vxe-input"),g=v("vxe-form-item"),S=v("vxe-form");return A(),j(a(X),U(m.$attrs,{loadingText:a(e).loadingText,showFooter:"",title:"\u5B57\u6BB5\u4FE1\u606F",width:"70%",onOk:i[0]||(i[0]=t=>a(Z)(m.$refs.xForm,a(e),n,a(_),a(u),a(h))),onRegister:a(B)}),{default:l(()=>[r(S,{"title-colon":"",ref:(t,o)=>{o.xForm=t},"title-align":"right","title-width":"100",data:a(e).data,rules:a(e).rules},{default:l(()=>[r(g,{title:"\u5B57\u6BB5\u540D",field:"name","item-render":{},span:"12","title-width":"150"},{default:l(({data:t})=>[r(p,{modelValue:t.name,"onUpdate:modelValue":o=>t.name=o,clearable:""},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),r(g,{title:"\u6CE8\u91CA",field:"comet","item-render":{},span:"12","title-width":"150"},{default:l(({data:t})=>[r(p,{modelValue:t.comet,"onUpdate:modelValue":o=>t.comet=o,clearable:""},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),r(g,{title:"\u5907\u6CE8",field:"notes","item-render":{},span:"24","title-width":"150"},{default:l(({data:t})=>[r(p,{modelValue:t.notes,"onUpdate:modelValue":o=>t.notes=o,clearable:""},null,8,["modelValue","onUpdate:modelValue"])]),_:1}),r(g,{title:"\u521B\u5EFA\u65F6\u95F4",field:"crtim","item-render":{},span:"12","title-width":"150"},{default:l(({data:t})=>[w("span",null,T(t.crtim),1)]),_:1}),r(g,{title:"\u521B\u5EFA\u4EBA",field:"upman","item-render":{},span:"12","title-width":"150"},{default:l(({data:t})=>{var o;return[w("span",null,T((o=t.crman)==null?void 0:o.name),1)]}),_:1}),r(g,{title:"\u4FEE\u6539\u65F6\u95F4",field:"crtim","item-render":{},span:"12","title-width":"150"},{default:l(({data:t})=>[w("span",null,T(t.uptim),1)]),_:1}),r(g,{title:"\u4FEE\u6539\u4EBA",field:"upman","item-render":{},span:"12","title-width":"150"},{default:l(({data:t})=>{var o;return[w("span",null,T((o=t.upman)==null?void 0:o.name),1)]}),_:1})]),_:1},8,["data","rules"])]),_:1},16,["loadingText","onRegister"])}}}),ae={style:{color:"#0915f1","margin-left":"2px","margin-right":"10px",display:"inline-block"}},ne=$("\u67E5 \u8BE2"),re=$("\u65B0 \u589E"),ie={name:"BiFieldMain",inheritAttrs:!1};function se(d){var y;const n=ee(),e=k((y=n.params)==null?void 0:y.id),B=k({});function _(){return F(this,null,function*(){B.value=yield V.get({url:"/bi/table/main/one/"+e.value})})}const u=D({total:0,currentPage:1,pageSize:10}),h=k({}),m=D({name:"",tableid:e}),i=D({border:!0,resizable:!0,showHeaderOverflow:!0,showOverflow:!0,highlightHoverRow:!0,keepSource:!0,id:"full_edit_1",height:600,rowId:"id",data:[{id:10001,name:"Test1",nickname:"T1",role:"Develop",sex:"1",age:28,address:"Shenzhen"},{id:10002,name:"Test2",nickname:"T2",role:"Test",sex:"0",age:22,address:"Guangzhou"},{id:10003,name:"Test3",nickname:"T3",role:"PM",sex:"1",age:32,address:"Shanghai"},{id:10004,name:"Test4",nickname:"T4",role:"Designer",sex:"0",age:23,address:"Shenzhen"},{id:10005,name:"Test5",nickname:"T5",role:"Develop",sex:"0",age:30,address:"Shanghai"}],customConfig:{storage:!0,checkMethod({column:c}){return!["nickname","role"].includes(c.property)}},sortConfig:{trigger:"cell",remote:!0},filterConfig:{remote:!0},pagerConfig:{pageSize:10,pageSizes:[5,10,15,20,50,100,200,500,1e3]},toolbarConfig:{slots:{buttons:"tbtns"},refresh:!0,custom:!0},columns:[{type:"checkbox",align:"center",width:42,fixed:"left"},{type:"seq",align:"center",width:50,fixed:"left"},{field:"name",title:"\u5B57\u6BB5\u540D",width:300,sortable:!0,editRender:{name:"input"}},{field:"comet",title:"\u5B57\u6BB5\u6CE8\u91CA",width:300,sortable:!0,editRender:{name:"input"}},{field:"type",title:"\u5B57\u6BB5\u7C7B\u578B",width:150,editRender:{name:"$select",options:[],props:{placeholder:"\u8BF7\u9009\u62E9\u7C7B\u578B"}}},{field:"length",title:"\u5B57\u6BB5\u957F\u5EA6",width:100,editRender:{name:"$input",props:{type:"number"}}},{field:"comet",title:"\u5176\u4ED6\u8BF4\u660E",editRender:{name:"input"}},{field:"crtim",title:"\u521B\u5EFA\u65F6\u95F4",width:160,visible:!1,formatter({cellValue:c}){return M.toDateString(c,"yyyy-MM-dd")}},{field:"uptim",title:"\u66F4\u65B0\u65F6\u95F4",width:160,visible:!1,formatter({cellValue:c}){return M.toDateString(c,"yyyy-MM-dd HH:ss:mm")}}],checkboxConfig:{range:!0},editRules:{name:[{required:!0,message:"\u5B57\u6BB5\u540D\u5FC5\u987B\u586B\u5199"}]},editConfig:{trigger:"click",mode:"row",showStatus:!0}}),p=c=>F(this,null,function*(){const s=h.value,x={sex:"1",date12:"2021-01-01"},{row:b}=yield s.insertAt(x,c);yield s.setActiveCell(b,"name")}),g=({currentPage:c,pageSize:s})=>{u.currentPage=c,u.pageSize=s,S()},S=()=>{i.loading=!0,setTimeout(()=>{i.loading=!1,u.total=10,i.data=[{id:10001,name:"Test1",nickname:"T1",role:"Develop",sex:"1",age:28,address:"Shenzhen"},{id:10002,name:"Test2",nickname:"T2",role:"Test",sex:"0",age:22,address:"Guangzhou"},{id:10003,name:"Test3",nickname:"T3",role:"PM",sex:"1",age:32,address:"Shanghai"},{id:10004,name:"Test4",nickname:"T4",role:"Designer",sex:"0",age:23,address:"Shenzhen"},{id:10005,name:"Test5",nickname:"T5",role:"Develop",sex:"0",age:30,address:"Shanghai"},{id:10006,name:"Test6",nickname:"T6",role:"Develop",sex:"0",age:27,address:"Shanghai"},{id:10007,name:"Test7",nickname:"T7",role:"Develop",sex:"1",age:29,address:"Shenzhen"},{id:10008,name:"Test8",nickname:"T8",role:"Develop",sex:"0",age:32,address:"Shanghai"},{id:10009,name:"Test9",nickname:"T9",role:"Develop",sex:"1",age:30,address:"Shenzhen"},{id:10010,name:"Test10",nickname:"T10",role:"Develop",sex:"0",age:34,address:"Shanghai"}]},300)};H(()=>{const c=[{label:"\u5973",value:"0"},{label:"\u7537",value:"1"}],{columns:s}=i;if(s){const x=s[4];x&&x.editRender&&(x.editRender.options=c)}});const t=()=>{h.value.commitProxy("query")};H(()=>{_()});const[o,{openDrawer:oe}]=Y();return(c,s)=>{const x=v("vxe-input"),b=v("vxe-button"),N=v("vxe-pager"),G=v("vxe-grid");return A(),j(a(K),{class:"ztable",contentFullHeight:""},{default:l(()=>[r(G,U({ref:(f,q)=>{q.xGrid=f,h.value=f}},a(i)),{tbtns:l(()=>[w("span",ae,"\u8868\u540D\uFF1A"+T(B.value.name),1),r(x,{modelValue:a(m).name,"onUpdate:modelValue":s[0]||(s[0]=f=>a(m).name=f),placeholder:"\u8F93\u5165\u540D\u79F0\u67E5\u8BE2"},null,8,["modelValue"]),r(b,{onClick:t,status:"primary"},{default:l(()=>[ne]),_:1}),r(b,{onClick:s[1]||(s[1]=f=>p(-1))},{default:l(()=>[re]),_:1})]),pager:l(()=>[r(N,{layouts:["Sizes","PrevJump","PrevPage","Number","NextPage","NextJump","FullJump","Total"],"current-page":a(u).currentPage,"onUpdate:current-page":s[2]||(s[2]=f=>a(u).currentPage=f),"page-size":a(u).pageSize,"onUpdate:page-size":s[3]||(s[3]=f=>a(u).pageSize=f),total:a(u).total,onPageChange:g},null,8,["current-page","page-size","total"])]),_:1},16),r(te,{onRegister:a(o),onSuccess:c.handleSuccess},null,8,["onRegister","onSuccess"])]),_:1})}}const Be=R(P(z({},ie),{setup:se}));export{Be as default};