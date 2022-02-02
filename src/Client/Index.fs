module Index

open Elmish
open Fable.Core
open Fable.Remoting.Client
open Shared

type Model = { Todos: Todo list; Input: string }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init () : Model * Cmd<Msg> =
    let model = { Todos = []; Input = "" }

    let cmd =
        Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | GotTodos todos -> { model with Todos = todos }, Cmd.none
    | SetInput value -> { model with Input = value }, Cmd.none
    | AddTodo ->
        let todo = Todo.create model.Input

        let cmd =
            Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { model with Input = "" }, cmd
    | AddedTodo todo ->
        { model with
              Todos = model.Todos @ [ todo ] },
        Cmd.none

open Feliz
open Feliz.Bulma

let navBrand =
    Bulma.navbarBrand.div [
        Bulma.navbarItem.a [
            prop.href "https://safe-stack.github.io/"
            navbarItem.isActive
            prop.children [
                Html.img [
                    prop.src "/favicon.png"
                    prop.alt "Logo"
                ]
            ]
        ]
    ]

let containerBox (model: Model) (dispatch: Msg -> unit) =
    Bulma.box [
        Bulma.content [
            Html.ol [
                for todo in model.Todos do
                    Html.li [ prop.text todo.Description ]
            ]
        ]
        Bulma.field.div [
            field.isGrouped
            prop.children [
                Bulma.control.p [
                    control.isExpanded
                    prop.children [
                        Bulma.input.text [
                            prop.value model.Input
                            prop.placeholder "What needs to be done?"
                            prop.onChange (fun x -> SetInput x |> dispatch)
                        ]
                    ]
                ]
                Bulma.control.p [
                    Bulma.button.a [
                        color.isPrimary
                        prop.disabled (Todo.isValid model.Input |> not)
                        prop.onClick (fun _ -> dispatch AddTodo)
                        prop.text "Add"
                    ]
                ]
            ]
        ]
    ]

// These lines does not do anything?
open Fable.Core.JsInterop
importAll "bulma/bulma.sass" // confirmed imported here: styles dissapears when commented out
importAll "@creativebulma/bulma-collapsible/dist/css/bulma-collapsible.min.css" // confirmed imported here: if I add a style to the min.css, the style is applied
let bulmaCollapsible: obj = importDefault "@creativebulma/bulma-collapsible/dist/js/bulma-collapsible.min.js" // console error if mispelled, but nothing works yet
bulmaCollapsible?attach() |> ignore
//
//type BulmaCollapsible<'T> =
//  abstract value: 'T with get, set
//  abstract isAwesome: unit -> bool
//type BulmaCollapsibleStatic =
//  abstract Attach: string -> BulmaCollapsible<'T> list
//
//[<ImportDefault("@creativebulma/bulma-collapsible/dist/js/bulma-collapsible.min.js")>]
//let MyClass : BulmaCollapsibleStatic = jsNative
//let lol = MyClass.Attach("#collapsible-message-accordion-1")
let collapsible =
    Html.div [
        prop.id "accordion_first"
        prop.children [
            Html.article [
                prop.className "message"
                prop.children [
                    Html.div [
                        prop.className "message-header"
                        prop.children [
                            Html.p [
                                Html.text "Question 1"
                                Html.a [
                                    prop.custom ("data-action", "collapse")
                                    prop.href "#collapsible-message-accordion-1"
                                    prop.text "Collapse/Expand"
                                ]
                            ]
                        ]
                    ]
                    Html.div [
                        prop.classes [ "message-body"; "is-collapsible" ]
                        prop.custom ("data-parent", "accordion_first")
                        prop.id "collapsible-message-accordion-1"
                        prop.children [
                            Html.div [
                                prop.className "message-body-content"
                                prop.children [
                                    Html.text "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                                    Html.strong "Pellentesque risus mi"
                                    Html.text ", tempus quis placerat ut, porta nec nulla. Vestibulum rhoncus ac ex sit amet fringilla. Nullam gravida purus diam, et dictum "
                                    Html.text " efficitur. Aenean ac "
                                    Html.em "eleifend lacus"
                                    Html.text ", in mollis lectus. Donec sodales, arcu et sollicitudin porttitor, tortor urna tempor ligula, id porttitor mi magna a neque. Donec dui urna, vehicula et sem eget, facilisis sodales sem."
                                ]
                            ]
                        ]
                    ]
                ]
            ]
            Html.article [
                prop.className "message"
                prop.children [
                    Html.div [
                        prop.className "message-header"
                        prop.children [
                            Html.p [
                                Html.text "Question 2 "
                                Html.a [
                                    prop.custom ("data-action", "collapse")
                                    prop.href "#collapsible-message-accordion-2"
                                    prop.text "Collapse/Expand"
                                ]
                            ]
                        ]
                    ]
                    Html.div [
                        prop.classes [ "message-body"; "is-collapsible" ]
                        prop.custom ("data-parent", "accordion_first")
                        prop.id "collapsible-message-accordion-2"
                        prop.children [
                            Html.div [
                                prop.className "message-body-content"
                                prop.children [
                                    Html.text "Lorem ipsum dolor sit amet, consectetur adipiscing elit. "
                                    Html.strong "Pellentesque risus mi"
                                    Html.text ", tempus quis placerat ut, porta nec nulla. Vestibulum rhoncus ac ex sit amet fringilla. Nullam gravida purus diam, et dictum "
                                    Html.text " efficitur. Aenean ac "
                                    Html.em "eleifend lacus"
                                    Html.text ", in mollis lectus. Donec sodales, arcu et sollicitudin porttitor, tortor urna tempor ligula, id porttitor mi magna a neque. Donec dui urna, vehicula et sem eget, facilisis sodales sem."
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
    ]

let collapsible2 =
    Html.div [
        prop.className "card"
        prop.children [
            Html.header [
                prop.className "card-header"
                prop.children [
                    Html.p [
                        prop.className "card-header-title"
                        prop.text "Card title "
                    ]
                    Html.a [
                        prop.ariaLabel "more options"
                        prop.classes [ "card-header-icon"; "is-hidden-fullscreen" ]
                        prop.custom ("data-action", "collapse")
                        prop.href "#collapsible-card"
                        prop.children [
                            Html.span [
                                prop.className "icon"
                                prop.children [
                                    Html.i [
                                        prop.classes [ "fas"; "fa-angle-down" ]
                                        prop.custom("aria-hidden", "true")
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
            Html.div [
                prop.classes [ "is-collapsible"; "is-active" ]
                prop.id "collapsible-card"
                prop.children [
                    Html.div [
                        prop.className "card-content"
                        prop.children [
                            Html.p [
                                prop.classes [ "title"; "is-4" ]
                                prop.text "“There are two hard things in computer science: cache invalidation, naming things, and off-by-one errors.” "
                            ]
                            Html.p [
                                prop.classes [ "subtitle"; "is-5" ]
                                prop.text " Jeff Atwood"
                            ]
                        ]
                    ]
                    Html.footer [
                        prop.className "card-footer"
                        prop.children [
                            Html.p [
                                prop.className "card-footer-item"
                                prop.children [
                                    Html.span [
                                        Html.text "View on "
                                        Html.a [
                                            prop.href "https://twitter.com/codinghorror/status/506010907021828096"
                                            prop.text "Twitter"
                                        ]
                                    ]
                                ]
                            ]
                            Html.p [
                                prop.className "card-footer-item"
                                prop.children [
                                    Html.span [
                                        Html.text "Share on "
                                        Html.a [
                                            prop.href "#"
                                            prop.text "Facebook"
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
   ]
let view (model: Model) (dispatch: Msg -> unit) =
    Bulma.hero [
        hero.isFullHeight
        color.isPrimary
        prop.style [
            style.backgroundSize "cover"
            style.backgroundImageUrl "https://unsplash.it/1200/900?random"
            style.backgroundPosition "no-repeat center center fixed"
        ]
        prop.children [
            Bulma.heroHead [
                Bulma.navbar [
                    Bulma.container [ navBrand ]
                ]
            ]
            Bulma.heroBody [
                Bulma.container [
                    Bulma.column [
                        column.is6
                        column.isOffset3
                        prop.children [
                            Bulma.title [
                                text.hasTextCentered
                                prop.text "safe_bulma_collapsible"
                                prop.className "make-red"
                            ]
                            collapsible
                            collapsible2
                        ]
                    ]
                ]
            ]
        ]
    ]
